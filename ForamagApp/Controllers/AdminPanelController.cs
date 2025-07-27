using Foramag.Data;
using Foramag.Models;
using Microsoft.AspNetCore.Mvc;

namespace Foramag.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin";
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Home");
            var users = _context.t_Login.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Login model)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Home");

            if (ModelState.IsValid)
            {
                // Optional: check uniqueness
                if (_context.t_Login.Any(u => u.SlpCode == model.SlpCode))
                {
                    ModelState.AddModelError("SlpCode", "Ce SlpCode existe déjà.");
                    return View(model);
                }

                _context.t_Login.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int slpCode)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Home");

            var user = _context.t_Login.FirstOrDefault(u => u.SlpCode == slpCode);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(Login model)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Home");

            var user = _context.t_Login.FirstOrDefault(u => u.SlpCode == model.SlpCode);
            if (user == null) return NotFound();

            if (ModelState.IsValid)
            {
                user.Username = model.Username;
                user.Password = model.Password;
                user.IsAdmin = model.IsAdmin;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int slpCode)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Home");

            var user = _context.t_Login.FirstOrDefault(u => u.SlpCode == slpCode);
            if (user == null) return NotFound();

            _context.t_Login.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}