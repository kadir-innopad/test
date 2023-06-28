using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PorductMvc.Models;
using PorductMvc.Repository;
using PorductMvc.ViewModel;
using System;
using System.Diagnostics;
using System.Web.Providers.Entities;

namespace PorductMvc.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        Uri baseaddress = new Uri("https://localhost:44397/api/User");
        HttpClient client = new HttpClient();
        public HomeController()
        {
                client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            UserRepository userRepository = new UserRepository();
           
            List<UserViewModel> listofuser = new List<UserViewModel>();
            HttpResponseMessage httpResponseMessage = userRepository.GetResponse("User");
            string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
            listofuser = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            //client.BaseAddress = new Uri("https://localhost:44397/api/User");
            //var response = client.GetAsync("User");
            //response.Wait();
            //var test = response.Result;

            //listofuser = display.Result;
            return View(listofuser);

        }
        [HttpGet]
        public IActionResult Create( )
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel)
        {
            UserRepository userRepository = new UserRepository();
            HttpResponseMessage httpResponseMessage = userRepository.PostResponse("User/api/User/Create", userViewModel);
            httpResponseMessage.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
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