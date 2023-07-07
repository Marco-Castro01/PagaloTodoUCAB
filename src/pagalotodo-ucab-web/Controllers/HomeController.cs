using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string endpoint = "https://localhost:5001/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return View("401");
            }
            var userRole = HttpContext.Session.GetString("userrole");
            if(userRole.Equals("AdminEntity"))
            {
                return View("Privacy");

            }
            else if (userRole.Equals("PrestadorServicioEntity"))
            {
                
                return View("HomePrestador"); 
            }
            return View("HomeConsumidor");
            List<ServicioModel>? servicios = new List<ServicioModel>();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync(endpoint+"servicios"))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    servicios = JsonConvert.DeserializeObject<List<ServicioModel>>(responseContent);
                    return View(servicios);
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}