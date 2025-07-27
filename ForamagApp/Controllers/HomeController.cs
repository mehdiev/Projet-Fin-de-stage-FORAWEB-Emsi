using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForamagApp.Models;
using Microsoft.AspNetCore.Http;
using Foramag.Data;

namespace ForamagApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int[] selectedMonths = null)
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != "true")
                return RedirectToAction("Login", "Account");

            var slpCodeStr = HttpContext.Session.GetString("slpCode");

            if (string.IsNullOrEmpty(slpCodeStr))
            {
                ViewBag.TotalYear = 0;
                ViewBag.FilteredTotal = 0;
                ViewBag.MonthLabels = new List<string>();
                ViewBag.MonthValues = new List<decimal>();
                ViewBag.SelectedMonths = selectedMonths ?? Array.Empty<int>();
                return View();
            }

            int userSlpCode = Convert.ToInt32(slpCodeStr);
            int currentYear = DateTime.Now.Year;

            var data = _context.V_CA_COMM
                .Where(c => c.SlpCode == userSlpCode && c.Year == currentYear);


            decimal totalYear = data.Sum(c => c.Total_HT);

            decimal filteredTotal = (selectedMonths != null && selectedMonths.Any())
                ? data.Where(c => selectedMonths.Contains(c.Month)).Sum(c => c.Total_HT)
                : totalYear;

            var filteredData = (selectedMonths != null && selectedMonths.Any())
                ? data.Where(c => selectedMonths.Contains(c.Month))
                : data;

            var orderedMonthly = filteredData
                .GroupBy(c => c.Month)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Label = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Value = g.Sum(x => x.Total_HT)
                }).ToList();

            ViewBag.TotalYear = totalYear;
            ViewBag.FilteredTotal = filteredTotal;
            ViewBag.MonthLabels = orderedMonthly.Select(m => m.Label).ToList();
            ViewBag.MonthValues = orderedMonthly.Select(m => m.Value).ToList();
            ViewBag.SelectedMonths = selectedMonths ?? Array.Empty<int>();

            return View();
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != "true")
                return RedirectToAction("Login", "Account");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}