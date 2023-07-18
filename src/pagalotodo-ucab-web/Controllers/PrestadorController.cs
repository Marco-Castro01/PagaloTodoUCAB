using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoWeb.Models;
using UCABPagaloTodoWeb.Requests;

namespace UCABPagaloTodoWeb.Controllers
{
    public class PrestadorController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly string endpoint = "https://localhost:5001/";

        public PrestadorController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [Route("Prestador/pagosRecibidosServ/{serviDatos}")]

        public async Task<IActionResult> PagosRecibidos(string serviDatos)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }
                Guid id = new Guid(serviDatos.Split(";")[0]);
                string servicioName = serviDatos.Split(";")[1];
                List<PagoModel> pagos = new List<PagoModel>();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync(endpoint + "pagosPorServicio/" + id + "/pagos_recibidos"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        pagos = JsonConvert.DeserializeObject<List<PagoModel>>(responseContent);
                        ViewBag.Message = servicioName;

                        return View(pagos);
                    }
                }

            }
            catch (HttpRequestException ex)
            {
                // Capturar excepciones de solicitud HTTP
                ViewBag.Error = $"Error al hacer la solicitud HTTP: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Capturar excepciones generales
                ViewBag.Error = $"Error general: {ex.Message}";
            }
            return View();
        }


        public async Task<IActionResult> Mis_Servicios()
        {
            var token = HttpContext.Session.GetString("token");
            
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }
                Guid userId = new Guid(HttpContext.Session.GetString("userid"));

                List<ServicioModel> servicios = new List<ServicioModel>();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync(endpoint + "servicios"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        servicios = JsonConvert.DeserializeObject<List<ServicioModel>>(responseContent);
                        return View("Mis_Servicios", servicios.Where(x=>x.prestadorServicioId==userId).ToList());
                    }
                }

            }
            catch (HttpRequestException ex)
            {
                // Capturar excepciones de solicitud HTTP
                ViewBag.Error = $"Error al hacer la solicitud HTTP: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Capturar excepciones generales
                ViewBag.Error = $"Error general: {ex.Message}";
            }
            return View();
        }


    }
}
