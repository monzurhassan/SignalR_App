using Microsoft.AspNetCore.Mvc;
using SignalRApp.Models;
using System.Diagnostics;

namespace SignalRApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string username)
        {           
            if (!string.IsNullOrEmpty(username))
            {
                ViewData["Username"] = username;
            }
            else
            {
                ViewData["Username"] = HttpContext.Session.GetString("username");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Access");
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
        public IActionResult ShowConnectedUsers()
        {
            return View();
        }

    }
}
