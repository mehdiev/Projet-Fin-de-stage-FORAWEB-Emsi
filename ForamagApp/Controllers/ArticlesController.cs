using Foramag.Data;
using Foramag.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using X.PagedList;

namespace Foramag.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            // ✅ Load dropdown from V_Marque
            var marques = _context.V_Marque
                .OrderBy(m => m.ItmsGrpNam)
                .Select(m => m.ItmsGrpNam)
                .Distinct()
                .ToList();
            ViewBag.Marques = marques;

            var articles = _context.Articles
                .OrderBy(a => a.ItemName)
                .ToPagedList(pageNumber, pageSize);

            return View(articles);
        }

        [HttpPost]
        public IActionResult Index(string marque, string reference, string designation, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            // ✅ Load dropdown from V_Marque
            var marques = _context.V_Marque
                .OrderBy(m => m.ItmsGrpNam)
                .Select(m => m.ItmsGrpNam)
                .Distinct()
                .ToList();
            ViewBag.Marques = marques;

            bool hasInput = !string.IsNullOrWhiteSpace(marque)
                || !string.IsNullOrWhiteSpace(reference)
                || !string.IsNullOrWhiteSpace(designation);

            var query = _context.Articles.AsQueryable();

            if (hasInput)
            {
                if (!string.IsNullOrWhiteSpace(marque))
                    query = query.Where(a => a.Marque.Trim() == marque.Trim());

                if (!string.IsNullOrWhiteSpace(reference))
                    query = query.Where(a => a.ItemCode.StartsWith(reference));

                if (!string.IsNullOrWhiteSpace(designation))
                    query = query.Where(a => a.ItemName.Contains(designation));
            }

            var results = query
                .OrderBy(a => a.ItemName)
                .ToPagedList(pageNumber, pageSize);

            return View(results);
        }
    }
}