using Foramag.Data;
using ForamagApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace ForamagApp.Controllers
{
    public class RechercheClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RechercheClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var rechercheclients = _context.Clients
                .OrderBy(c => c.Intitule_Client)
                .ToPagedList(pageNumber, pageSize);

            return View(rechercheclients);
        }

        [HttpPost]
        public IActionResult Index(string CodeClient, string NomClient, string AncienCodeClient, int? page)
        {
            ViewBag.CodeClient = CodeClient;
            ViewBag.NomClient = NomClient;
            ViewBag.AncienCodeClient = AncienCodeClient;

            int pageSize = 20;
            int pageNumber = page ?? 1;

            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(CodeClient))
                query = query.Where(c => c.Code_Client.StartsWith(CodeClient));

            if (!string.IsNullOrWhiteSpace(NomClient))
                query = query.Where(c => c.Nom_Client.Contains(NomClient));

            if (!string.IsNullOrWhiteSpace(AncienCodeClient))
                query = query.Where(c => c.Ancien_Code.StartsWith(AncienCodeClient));

            var result = query
                .OrderBy(c => c.Intitule_Client)
                .ToPagedList(pageNumber, pageSize);

            return View(result);
        }
    }
}