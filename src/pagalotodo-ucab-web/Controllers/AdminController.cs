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




        public ViewResult Modificar_Servicio(ServicioModel servicio)
        {
            var token = HttpContext.Session.GetString("token");

            return View(servicio);
        }


        [HttpPost]
        public async Task<IActionResult> Modificar_Servicio(Guid id, ServicioModel servicio)
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


                    StringContent content = new StringContent(JsonConvert.SerializeObject(servicio), Encoding.UTF8, "application/json");


                    using (var response = await httpClient.PatchAsync("https://localhost:5001/servicio/"+servicio.Id.ToString()+"/update", content))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("InformacionServicioAsync", "Admin", new { id = servicio.Id });
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

            ServicioModel servicioModel = new ServicioModel
            {
                Id = servicio.Id,
                name = servicio.name,
                accountNumber=servicio.accountNumber,
                tipoServicio = servicio.tipoServicio,
                statusServicio = servicio.statusServicio
            };

            // Pasar el modelo a la vista
            return View(servicioModel);
        }



        [Route("Admin/InformacionPrestadorAsync/{id}")]

        public async Task<IActionResult> InformacionPrestadorAsync(Guid Id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                // Si no se encuentra el token en la sesión, lanzar una excepción
                throw new Exception("No se encontró el token en la sesión.");
            }


            try
            {
                //var token = HttpContext.Session.GetString("token");
               
                PrestadorModel Prestador = new PrestadorModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync("https://localhost:5001/prestador_servicio/" + Id.ToString() + "/info"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        Prestador = JsonConvert.DeserializeObject<PrestadorModel>(responseContent);
                        
                    }
                    List<ServicioModel> servicios=new List<ServicioModel>();   
                    using (var ResponseS = await httpClient.GetAsync("https://localhost:5001/servicios"))
                    {
                        ResponseS.EnsureSuccessStatusCode();

                        var responseContent = await ResponseS.Content.ReadAsStringAsync();
                        servicios = JsonConvert.DeserializeObject<List<ServicioModel>>(responseContent);

                    }
                    Prestador.servicios = servicios.Where(s => s.prestadorServicioId == Prestador.Id).ToList();

                    return View("InformacionPrestadorAsync",Prestador);
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

            // Agregar una instrucción de retorno por defecto, puede ser una vista específica o null según tus necesidades
            return View("../Home/401"); // Por ejemplo, devuelve una vista de error
        }


        [Route("Admin/InformacionServicioAsync/{id}")]

        public async Task<IActionResult> InformacionServicioAsync(Guid Id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                // Si no se encuentra el token en la sesión, lanzar una excepción
                return View("../Home/401"); // Por ejemplo, devuelve una vista de error
            }


            try
            {
                ServicioModel servicio = new ServicioModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    List<ServicioModel> servicios = new List<ServicioModel>();
                    using (var ResponseS = await httpClient.GetAsync("https://localhost:5001/servicios"))
                    {
                        ResponseS.EnsureSuccessStatusCode();

                        var responseContent = await ResponseS.Content.ReadAsStringAsync();
                        servicios = JsonConvert.DeserializeObject<List<ServicioModel>>(responseContent);

                    }
                    servicio = servicios.FirstOrDefault(s => s.Id == Id);


                    return View("InformacionServicioAsync", servicio);
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

            // Agregar una instrucción de retorno por defecto, puede ser una vista específica o null según tus necesidades
            return View("../Home/401"); // Por ejemplo, devuelve una vista de error
        }


        [Route("Admin/CierreContable/{id}")]

        public async Task<IActionResult> CierreContable(Guid Id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                // Si no se encuentra el token en la sesión, lanzar una excepción
                throw new Exception("No se encontró el token en la sesión.");
            }


            try
            {
                //var token = HttpContext.Session.GetString("token");

                PrestadorModel Prestador = new PrestadorModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync("https://localhost:5001/prestador_servicio/" + Id.ToString() + "/info"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        Prestador = JsonConvert.DeserializeObject<PrestadorModel>(responseContent);

                    }
                   
                    List<CierreContableModel> cierres = new List<CierreContableModel>();
                    using (var ResponseS = await httpClient.GetAsync("https://localhost:5001/prestador_servicio/"+Id+"/cierreContable"))
                    {
                        ResponseS.EnsureSuccessStatusCode();

                        var responseContent = await ResponseS.Content.ReadAsStringAsync();
                        cierres = JsonConvert.DeserializeObject<List<CierreContableModel>>(responseContent);

                    }
                    Prestador.cierres = cierres.ToList();

                    return View("CierreContable", Prestador);
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

            // Agregar una instrucción de retorno por defecto, puede ser una vista específica o null según tus necesidades
            return View("../Home/401"); // Por ejemplo, devuelve una vista de error
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
        public async Task<IActionResult> RegistrarPrestador(NewPrestadorModel usuario)
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


        //TODO: TERMINAR
        public ViewResult RegistrarServicioAPrestador() => View();
        [HttpPost]
        public async Task<IActionResult> RegistrarServicioAPrestador(AdminModel usuario)
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


        public async Task<IActionResult> Report_PrestadoresRegistrados()
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }

                List<PrestadorModel> prestador = new List<PrestadorModel>();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync("https://localhost:5001/prestadores_servicios"))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseContent = await response.Content.ReadAsStringAsync();
                        prestador = JsonConvert.DeserializeObject<List<PrestadorModel>>(responseContent);
                        return View(prestador);
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
