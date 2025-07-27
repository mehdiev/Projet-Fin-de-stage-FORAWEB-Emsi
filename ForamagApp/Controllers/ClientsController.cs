using Foramag.Data;
using Foramag.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace Foramag.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var slpCodeStr = HttpContext.Session.GetString("slpCode");
            int userSlpCode = 0;
            int.TryParse(slpCodeStr, out userSlpCode);

            var allClients = _context.Clients
                .Where(c => c.SlpCode == userSlpCode)
                .OrderBy(c => c.Intitule_Client)
                .ToPagedList(pageNumber, pageSize);

            return View(allClients);
        }

        [HttpPost]
        public IActionResult Index(string nomClient, string slpName, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var slpCodeStr = HttpContext.Session.GetString("slpCode");
            int userSlpCode = 0;
            int.TryParse(slpCodeStr, out userSlpCode);

            var query = _context.Clients
                .Where(c => c.SlpCode == userSlpCode)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomClient))
                query = query.Where(c => c.Intitule_Client.Contains(nomClient));

            if (!string.IsNullOrWhiteSpace(slpName))
                query = query.Where(c => c.Intitule_Client.Contains(slpName));

            var results = query
                .OrderBy(c => c.Intitule_Client)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.LastSearch = nomClient;
            ViewBag.LastSlpSearch = slpName;

            return View(results);
        }

        [HttpGet]
        public IActionResult Detail(string code)
        {
            var clientDetails = _context.V_Clt_Detail
                .FirstOrDefault(c => c.CardCode == code);

            if (clientDetails == null)
                return NotFound();

            return View(clientDetails);
        }
    }
}