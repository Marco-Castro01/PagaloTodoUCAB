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
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly string endpoint = "https://localhost:44339/usuario";

        public LoginController(ILogger<LoginController> logger)
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
                        return RedirectToAction("GestionUsuarios", "Login");
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
                        return RedirectToAction("GestionUsuarios", "Login");
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







        public ViewResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel usuario)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario.Login), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint + "/login", content))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(responseContent);
                        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                        var userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                        var Role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                        var name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        HttpContext.Session.SetString("token", responseContent);
                        HttpContext.Session.SetString("userid", userId);
                        HttpContext.Session.SetString("useremail", userEmail);
                        HttpContext.Session.SetString("userrole", Role);
                        HttpContext.Session.SetString("username", name);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", "Error al realizar la solicitud HTTP");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error en el servidor");
            }
            return View(usuario);

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarConsumidor(LoginModel usuario)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("Index", new LoginModel { Consumidor= usuario.Consumidor });
                    }
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario.Consumidor), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint + "/Consumidor", content))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync();
                        JObject json_respuesta = JObject.Parse(responseContent);
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

            return View("Index", new LoginModel { Consumidor = usuario.Consumidor });
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
                        return RedirectToAction("GestionUsuarios", "Login");
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
                        return RedirectToAction("GestionUsuarios", "Login");
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




        public ViewResult Olvidocontrasena() => View();

        [HttpPost]
        public async Task<IActionResult> Olvidocontrasena(ResetPasswordModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint + "/resettoken", content))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync();
                        JObject json_respuesta = JObject.Parse(responseContent);

                         return RedirectToAction("ResetPassword", "Login"); ;
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

            return View();
        }
        public ViewResult ResetPassword() => View();
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ChangePasswordModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint + "/password", content))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync();

                        return RedirectToAction("Index", "Login"); ;
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

            return View();
        }

        public IActionResult Cerrar()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("token" );
            HttpContext.Session.Remove("userid");
            HttpContext.Session.Remove("useremail" );
            HttpContext.Session.Remove("userrole" );
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

    }
}
