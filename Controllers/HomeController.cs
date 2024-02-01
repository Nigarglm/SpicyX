using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;
using SpicyX.Models;

namespace SpicyX.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<IActionResult> Index()
        {
            List<Chef> chefs = await _context.Chefs.OrderBy(c=>c.Id).ToListAsync();
            List<Position> positions = await _context.Positions.ToListAsync();


            return View(chefs);
        }
    }
}
