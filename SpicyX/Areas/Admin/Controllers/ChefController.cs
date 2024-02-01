using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;
using SpicyX.Models;

namespace SpicyX.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ChefController : Controller
    {
        private readonly AppDbContext _context;
        public ChefController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Chef> chefs = await _context.Chefs.ToListAsync();

            return View(chefs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id)
        {
            if (!ModelState.IsValid) return View();

            //bool result = _context.
            return View();
        }
        public async Task<IActionResult> Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id)
        {
            return View();
        }
        public async Task<IActionResult> Details()
        {
            List<Chef> chefs = await _context.Chefs.ToListAsync();
            return View(chefs);
        }
        public async Task<IActionResult> Delete()
        {
            return View();
        }
    }
}
