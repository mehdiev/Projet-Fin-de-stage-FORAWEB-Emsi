using Foramag.Data;
using ForamagApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForamagApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var user = _context.t_Login 
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                // ✅ Store key session values after successful login
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("UserRole", user.IsAdmin ? "Admin" : "User");

                // ✅ Store slpCode for client filtering
                HttpContext.Session.SetString("slpCode", user.SlpCode.ToString());

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Identifiants incorrects";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}