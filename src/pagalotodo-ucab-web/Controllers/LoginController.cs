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
        private readonly string endpoint = "https://localhost:5001/usuario";

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
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
                   
                    using (var response = await httpClient.PostAsync("https://localhost:5001/usuario/login", content))
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

                        return RedirectToAction("Index", "Login"); 
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
        

       

    }
}
