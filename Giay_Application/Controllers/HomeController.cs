using Giay_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Giay_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanliBanGiayContext _db;

        public HomeController(ILogger<HomeController> logger, QuanliBanGiayContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.SanPhams.ToList();

            // Gửi dữ liệu sang View
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
