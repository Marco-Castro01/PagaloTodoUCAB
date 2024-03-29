﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoWeb.enums;
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



        public ViewResult EnviarArchivoConciliacionView()
        {
            
            return View();
        }



        [Route("Prestador/EnviarArchivoConciliacion")]

        [HttpPost]

        public async Task<IActionResult> EnviarArchivoConciliacion(IFormFile file)
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

                using (var httpClient = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        // Agregar el archivo al formulario
                        var fileContent = new StreamContent(file.OpenReadStream());
                        formData.Add(fileContent, "file", file.FileName);

                        // Enviar la solicitud HTTP al endpoint                    
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        using (var response = await httpClient.PostAsync(endpoint + "prestador/Archivo_conciliacion/enviar_Archivo", formData))
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            // Verificar el resultado de la solicitud
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["SuccessMessage"] = responseContent; // Guardar el mensaje de éxito en TempData
                                return RedirectToAction("EnviarArchivoConciliacionView", "Prestador");
                            }
                            else
                            {
                                // Manejar el error o mostrar una alerta con el mensaje de error
                                TempData["ErrorMessage"] = responseContent; // Guardar el mensaje de error en TempData
                                return RedirectToAction("EnviarArchivoConciliacionView", "Prestador");
                            }
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
            return View("EnviarArchivoConciliacionView");


        }





        public ViewResult EnviarArchivoVerificacionView(Guid idServicio, string servicioName)
        {
            ViewBag.Id = idServicio;
            ViewBag.servicioName = servicioName;
            return View("EnviarArchivoVerificacionView");
        }

       

        [HttpPost]

        public async Task<IActionResult> EnviarArchivoVerificacion(IFormFile file,Guid idServicio, string servicioName)
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
                Guid servicioId = idServicio;
                string nombreS = servicioName;
                using (var httpClient = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        // Agregar el archivo al formulario
                        var fileContent = new StreamContent(file.OpenReadStream());
                        formData.Add(fileContent, "file", file.FileName);

                        // Enviar la solicitud HTTP al endpoint                    
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        using (var response = await httpClient.PostAsync(endpoint + "prestador/archivoVerificacion/"+idServicio+"/enviarArchivo", formData))
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            // Verificar el resultado de la solicitud
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["SuccessMessage"] = responseContent; // Guardar el mensaje de éxito en TempData
                                return EnviarArchivoVerificacionView(idServicio, servicioName);
                            }
                            else
                            {
                                // Manejar el error o mostrar una alerta con el mensaje de error
                                TempData["ErrorMessage"] = responseContent; // Guardar el mensaje de error en TempData
                                return EnviarArchivoVerificacionView(idServicio, servicioName);
                            }
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
            return EnviarArchivoVerificacionView(idServicio, servicioName);


        }






        [HttpGet]
        public IActionResult ConfigurarCampoView(Guid servicioId)
        {
            // Crear el objeto CamposPagosModel y establecer el valor del ServicioId
            var model = new CamposPagosModel
            {
                servicioId = servicioId
            };

            // Pasar el modelo a la vista
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ConfigurarCampo(CamposPagosModel camposPagosModel)
        {
            string responseContent="";
            

            try
            {
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(token))
                {
                    // Si no se encuentra el token en la sesión, lanzar una excepción
                    throw new Exception("No se encontró el token en la sesión.");
                }
                if (camposPagosModel.TipoDato != TipoDato.conDecimal)
                {
                    if (camposPagosModel.TipoDato != TipoDato.entero)
                        camposPagosModel.separadorDeMiles = null;
                    camposPagosModel.separadorDeDecimales = null;
                }
                else
                {
                    if (camposPagosModel.separadorDeMiles == ".")
                    {
                        camposPagosModel.separadorDeDecimales = ",";
                    }
                    else
                    {
                        camposPagosModel.separadorDeDecimales = ".";

                    }
                }

                if (camposPagosModel.TipoDato != TipoDato.fecha)
                {
                    camposPagosModel.formatofecha = null;
                }

                if (camposPagosModel.TipoDato != TipoDato.hiperTexto)
                {
                    camposPagosModel.Longitud = 0;
                }
                

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    CamposPagosRequestModel campos=new CamposPagosRequestModel();
                    campos.Nombre = camposPagosModel.Nombre;
                    campos.separadorDeMiles = camposPagosModel.separadorDeMiles;
                    campos.separadorDeDecimales = camposPagosModel.separadorDeDecimales;
                    campos.TipoDato=camposPagosModel.TipoDato;
                    campos.formatofecha = camposPagosModel.formatofecha;
                    campos.Longitud = camposPagosModel.Longitud;
                    campos.inCOnciliacion = camposPagosModel.inCOnciliacion;
                    campos.contenido = camposPagosModel.contenido;

                    List<CamposPagosRequestModel> camposP = new List<CamposPagosRequestModel>();
                    camposP.Add(campos);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(camposP), Encoding.UTF8, "application/json");
                    

                    using (var response = await httpClient.PutAsync(endpoint + "servicio/"+camposPagosModel.servicioId+"/formatoPago", content))
                    {
                        response.EnsureSuccessStatusCode();

                        responseContent = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = responseContent; // Guardar el mensaje de éxito en TempData
                            return RedirectToAction("ConfigurarCampoView", camposPagosModel.servicioId);
                        }
                        else
                        {
                            // Manejar el error o mostrar una alerta con el mensaje de error
                            TempData["ErrorMessage"] = responseContent; // Guardar el mensaje de error en TempData
                            return RedirectToAction("ConfigurarCampoView", camposPagosModel.servicioId);
                        }
                    }

                }
            }
            catch (HttpRequestException ex)
            {
                // Capturar excepciones de solicitud HTTP
                TempData["ErrorMessage"] = "Error al agregar"; // Guardar el mensaje de error en TempData
                return RedirectToAction("ConfigurarCampoView", camposPagosModel.servicioId);
            }
            catch (Exception ex)
            {
                // Capturar excepciones generales
                TempData["ErrorMessage"] = responseContent; // Guardar el mensaje de error en TempData
                return RedirectToAction("ConfigurarCampoView", camposPagosModel.servicioId);
            }


        }



    }
}
