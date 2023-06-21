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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly string endpoint = "https://localhost:5001/usuario";

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        public ViewResult CambiarContrasena()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarContrasena( ActualizarContrasenaModel usuario)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    var userId = HttpContext.Session.GetString("userid");
                    if (string.IsNullOrEmpty(userId))
                    {
                        // Si no se encuentra el ID del usuario en la sesión, lanzar una excepción o manejar el error adecuadamente
                        throw new Exception("No se encontró el ID del usuario en la sesión.");
                    }
                    using (var response = await httpClient.PutAsync(endpoint + "/password/" + userId, content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index", "Home");
                    }

                }
                return View(usuario);
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


        public IActionResult Cerrar()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("userid");
            HttpContext.Session.Remove("useremail");
            HttpContext.Session.Remove("userrole");
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Login");
        }


    }
}
