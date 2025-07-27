using Foramag.Data;
using Foramag.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using X.PagedList;

namespace Foramag.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var allDocs = _context.Documents
                .OrderByDescending(d => d.Date_Document)
                .ToPagedList(pageNumber, pageSize);

            return View(allDocs);
        }

        [HttpPost]
        public IActionResult Index(string numDocument, string codeClient, string nomClient, DateTime? dateStart, DateTime? dateEnd, int? page)
        {
            ViewBag.LastNumSearch = numDocument;
            ViewBag.LastCodeSearch = codeClient;
            ViewBag.LastNomSearch = nomClient;
            ViewBag.LastStartDate = dateStart?.ToString("yyyy-MM-dd");
            ViewBag.LastEndDate = dateEnd?.ToString("yyyy-MM-dd");

            int pageSize = 20;
            int pageNumber = page ?? 1;

            var query = _context.Documents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(numDocument))
                query = query.Where(d => d.Num_Document.StartsWith(numDocument));

            if (!string.IsNullOrWhiteSpace(codeClient))
                query = query.Where(d => d.Code_Client.StartsWith(codeClient));

            if (!string.IsNullOrWhiteSpace(nomClient))
                query = query.Where(d => d.Nom_Client.StartsWith(nomClient));

            if (dateStart.HasValue && dateEnd.HasValue)
                query = query.Where(d => d.Date_Document.Date >= dateStart.Value.Date && d.Date_Document.Date <= dateEnd.Value.Date);
            else if (dateStart.HasValue)
                query = query.Where(d => d.Date_Document.Date >= dateStart.Value.Date);
            else if (dateEnd.HasValue)
                query = query.Where(d => d.Date_Document.Date <= dateEnd.Value.Date);

            var results = query
                .OrderByDescending(d => d.Date_Document)
                .ToPagedList(pageNumber, pageSize);

            return View(results);
        }

        [HttpGet]
        public IActionResult Detail(string num)
        {
            var doc = _context.Documents.FirstOrDefault(d => d.Num_Document == num);
            var lignes = _context.V_Doc_Lignes.Where(l => l.Num_Document == num).ToList();

            if (doc == null)
                return NotFound();

            ViewBag.Lignes = lignes;
            return View(doc);
        }
    }
}