using KayitProgrami.Data;
using KayitProgrami.Migrations;
using KayitProgrami.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KayitProgrami.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var kullaniciId = _userManager.GetUserId(User);  // Mevcut kullan�c� ID'sini al�r
            var izinTalepleri = _context.IzinTalepleri
                                        .Where(x => x.KullaniciId == kullaniciId)  // Kullan�c�ya ait izin taleplerini filtreler
                                        .Include(i => i.AspNetUsers)  // Kullan�c� bilgilerini dahil eder
                                        .ToList();

            return View(izinTalepleri);

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
    }
}
