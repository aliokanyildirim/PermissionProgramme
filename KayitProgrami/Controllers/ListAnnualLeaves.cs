using KayitProgrami.Data;
using KayitProgrami.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KayitProgrami.Controllers
{
    public class ListAnnualLeaves : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ListAnnualLeaves(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> ListAnnual(int id)
        {
           

            return View();
        }
    }        
}
