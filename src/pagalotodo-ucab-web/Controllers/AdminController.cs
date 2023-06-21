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
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly string endpoint = "https://localhost:5001/usuario";

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> GestionUsuarios()
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }

                List<UsuariosModel> usuarios = new List<UsuariosModel>();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync(endpoint))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        usuarios = JsonConvert.DeserializeObject<List<UsuariosModel>>(responseContent);
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

        public ViewResult Modificar(UsuariosModel usuario) {
            var token = HttpContext.Session.GetString("token");
         return  View(usuario);
        }


        [HttpPost]
        public async Task<IActionResult> Modificar(Guid id, UpdateUserModel usuario)
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


                    using (var response = await httpClient.PutAsync(endpoint + "/" +id, content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("GestionUsuarios", "Admin");
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

        public ViewResult CambiarContrasena(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(Guid id, ActualizarContrasenaModel usuario)
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


                    using (var response = await httpClient.PutAsync(endpoint + "/password/" + id, content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("GestionUsuarios", "Admin");
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


        public ViewResult RegistrarPrestador() => View();
        [HttpPost]
        public async Task<IActionResult> RegistrarPrestador(PrestadorModel usuario)
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
                    if (!ModelState.IsValid)
                    {
                        return View("RegistrarPrestador", usuario);
                    }
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint + "/Prestador", content))
                    {
                        response.EnsureSuccessStatusCode();
                        return RedirectToAction("GestionUsuarios", "Admin");
                        // Procesar la respuesta del servicio si es necesario
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "Error al realizar la solicitud HTTP");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "Ocurrió un error en el servidor");
            }

            return View("RegistrarPrestador", usuario);
        }
      
        public ViewResult RegistrarAdmin() => View();
        [HttpPost]
        public async Task<IActionResult> RegistrarAdmin(AdminModel usuario)
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
                    if (!ModelState.IsValid)
                    {
                        return View("RegistrarAdmin", usuario);
                    }
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint + "/admin", content))
                    {
                        response.EnsureSuccessStatusCode();
                        return RedirectToAction("GestionUsuarios", "Admin");
                        // Procesar la respuesta del servicio si es necesario
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "Error al realizar la solicitud HTTP");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "Ocurrió un error en el servidor");
            }

            return View("RegistrarAdmin", usuario);
        }
    }
}
