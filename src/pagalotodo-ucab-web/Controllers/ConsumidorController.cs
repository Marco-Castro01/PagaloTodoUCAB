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
    public class ConsumidorController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly string endpoint = "https://localhost:5001/";

        public ConsumidorController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> HistoricoPagos()
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }

                List<PagoModel> usuarios = new List<PagoModel>();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync(endpoint + "pagosPorConsumidor/pagosHechos"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        usuarios = JsonConvert.DeserializeObject<List<PagoModel>>(responseContent);
                        return View(usuarios);
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


        public async Task<IActionResult> Servicios()
        {
            var token = HttpContext.Session.GetString("token");
            
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }

                List<ServicioModel> servicios = new List<ServicioModel>();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync(endpoint + "servicios"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        servicios = JsonConvert.DeserializeObject<List<ServicioModel>>(responseContent);
                        return View("Servicios", servicios);
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




        public async Task<IActionResult> Modificar_PerfilView()
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
                    using (var response = await httpClient.GetAsync(endpoint + "consumidor/info"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        ConsumidorModel consumidor = JsonConvert.DeserializeObject<ConsumidorModel>(responseContent);
                        UsuariosModel usuario = new UsuariosModel
                        {
                            Id = consumidor.Id,
                            email=consumidor.email,
                            name = consumidor.name,
                            cedula = consumidor.cedula,
                            nickName = consumidor.nickName,
                            status = consumidor.status
                        };
                        return View("Modificar_PerfilView", usuario);
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


            // Pasar el modelo a la vista
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Modificar_Perfil(Guid id, UpdateUserModel usuario)
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


                    using (var response = await httpClient.PutAsync(endpoint +"consumidor/modificar", content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Modificar_PerfilView");
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

            UsuariosModel usuarioModel = new UsuariosModel
            {
                Id = id,
                name = usuario.name,
                cedula = usuario.cedula,
                rif = usuario.rif,
                nickName = usuario.nickName,
                status = usuario.status
            };

            // Pasar el modelo a la vista
            return View(usuarioModel);
        }





        [HttpPost]
        public async Task<IActionResult> ConsultarDeuda(Guid servicioId, string identificador)
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

                    GetDeudaModel getDeuda = new GetDeudaModel();
                    getDeuda.identificador = identificador;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(getDeuda), Encoding.UTF8, "application/json");


                    using (var response = await httpClient.PostAsync(endpoint + "servicio/"+ servicioId + "/ConsultarDeuda", content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        List<DeudaModel> deudas = JsonConvert.DeserializeObject<List<DeudaModel>>(responseContent);
                        return View("DeudasView", deudas);
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

           
            // Pasar el modelo a la vista
            return View();
        }



        public async Task<IActionResult> PagarView(Guid idDeuda)
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



                    using (var response = await httpClient.GetAsync(endpoint + "deudaInf/" + idDeuda))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        DeudaModel deuda = JsonConvert.DeserializeObject<DeudaModel>(responseContent);
                        return View("PagarView", deuda);
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


            // Pasar el modelo a la vista
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Pagar(InfoPagar pago)
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
                    DeudaModel deuda = new DeudaModel();


                    using (var response = await httpClient.GetAsync(endpoint + "deudaInf/" + pago.DeudaId))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        deuda = JsonConvert.DeserializeObject<DeudaModel>(responseContent);
                    }

                    for (int i = 0; i < deuda.camposPagos.Count; i++)
                    {
                        deuda.camposPagos[i].contenido = pago.CamposPagos[i];
                    }

                    PagarModel pagar = new PagarModel();
                    pagar.Valor = deuda.deuda;
                    pagar.camposPagos = deuda.camposPagos;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pagar), Encoding.UTF8, "application/json");


                    using (var response = await httpClient.PostAsync(endpoint + "Consumidor/pago/" + pago.DeudaId + "/pagar", content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                       
                        


                        if (response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = responseContent; // Guardar el mensaje de éxito en TempData
                            return RedirectToAction("Servicios");
                        }
                        else
                        {
                            // Manejar el error o mostrar una alerta con el mensaje de error
                            TempData["ErrorMessage"] = responseContent; // Guardar el mensaje de error en TempData
                            return RedirectToAction("Servicios");
                        }

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


            // Pasar el modelo a la vista
            return View();
        }

    //    [HttpPost]
    //    public IActionResult Pagar(InfoPagar pagoModel)
    //    {

    //        int deudaId = pagoModel.DeudaId;
    //        decimal valorDeuda = pagoModel.ValorDeuda;

    //        List<string> camposPagos = pagoModel.CamposPagos;
    //        foreach (var campo in camposPagos)
    //        {

    //        }

    //        return View("HomeConsumidor");
    //    }



    }
}
