using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PitangueiraController : Controller
    {
        private readonly PitangueiraContext _context;

        public PitangueiraController(PitangueiraContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" , accessToken);
            string jsonRes = await httpClient.GetStringAsync("https://localhost:44391/api/Employee/Task");

            if (jsonRes == null)
            {
                return View();
            }

            var res = JsonConvert.DeserializeObject<Employee>(jsonRes);
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonRes = await httpClient.GetStringAsync("https://localhost:44391/api/Employee/Types");
            var res = JsonConvert.DeserializeObject<List<AttenType>>(jsonRes);
            ViewData["TypeName"] = new SelectList(res, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Clients,Comments, Type")] AttendanceCreate Attendance)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            StringContent content = new StringContent(JsonConvert.SerializeObject(Attendance), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44391/api/Employee/CreateTask/", content);
            var status = response.IsSuccessStatusCode;
            if (status)
                return Redirect("/Pitangueira/Index/");
            return View();
        }

        public async Task<IActionResult> Types()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonRes = await httpClient.GetStringAsync("https://localhost:44391/api/Employee/TypesManagement");


            var res = JsonConvert.DeserializeObject<EmployeewTypes>(jsonRes);
            return View(res);
        }

        public IActionResult CreateType()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTypeAsync([Bind("Name")] AttenType attendanceType)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            StringContent content = new StringContent(JsonConvert.SerializeObject(attendanceType), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44391/api/Employee/CreateType/", content);
            var status = response.IsSuccessStatusCode;
            if (status)
                return Redirect("/Pitangueira/Types/");

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44391/api/Employee/DeleteTask/", content);
            return Redirect("/Pitangueira/Index/");
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44391/api/Employee/DeleteType/", content);
            return Redirect("/Pitangueira/Types/");
        }

        public async Task<IActionResult> Relatory()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null || String.Empty == accessToken)
                return Redirect("/Pitangueira/Logout/");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonRes = await httpClient.GetStringAsync("https://localhost:44391/api/Employee/AllTasks");


            var res = JsonConvert.DeserializeObject<EmployeeRelatory>(jsonRes);

            return View(res);
        }

        public IActionResult LogOut() 
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Index/");
        }

    }
}
