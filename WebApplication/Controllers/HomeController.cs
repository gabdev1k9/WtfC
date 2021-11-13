using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("UserName,Password")]User User)
        {
            using(var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
                using(var response = await httpClient.PostAsync("https://localhost:44391/api/Token",content))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if(token == "Error")
                    {
                        return Redirect("/Home/Index");
                    }
                    HttpContext.Session.SetString("JWToken", token);
                }
            }
            return Redirect("/Pitangueira/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
