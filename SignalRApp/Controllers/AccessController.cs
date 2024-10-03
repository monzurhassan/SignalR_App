using Microsoft.AspNetCore.Mvc;

namespace SignalRApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccessController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            bool isValid = true;
                     
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.UsernameError = "Please enter username";
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                ViewBag.PasswordError = "Please enter password";
                isValid = false;
            }

            if (!isValid)
            {              
                return View();
            }
                 
            var validUsers = new Dictionary<string, string>
            {
                { "user1", "1234" },
                { "user2", "1234" },
                { "user3", "1234" }
            };

            if (validUsers.ContainsKey(username) && validUsers[username] == password)
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Home");
            }

        
            ViewBag.Error = "password and user is incorrect";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
