using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyX.Areas.ViewModels.Chef;
using SpicyX.DAL;
using SpicyX.Models;
using SpicyX.Utilities.Extensions;

namespace SpicyX.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ChefController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ChefController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Chef> chefs = await _context.Chefs.ToListAsync();

            return View(chefs);
        }

        [Authorize("Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize("Admin,Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateChefVM chefVM)
        {
            if (!ModelState.IsValid) return View();

            bool result = await _context.Chefs.AnyAsync(c => c.Name.ToLower().Trim() == chefVM.Name.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adli chef artiq movcuddur");
                return View();
            }

            if (!chefVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }

            if (!chefVM.Photo.ValidateSize(2*1024))
            {
                ModelState.AddModelError("Photo", "File olchusuuygun deyil");
                return View();
            }

            string fileName = await chefVM.Photo.CreateFileAsync(_env.WebRootPath,"assets","img","chef");

            Chef chef = new Chef
            {
                Image = fileName,
                Name = chefVM.Name,
                Position = chefVM.Position,
                FbUrl = chefVM.FbUrl,
                TwitterUrl = chefVM.TwitterUrl,
                MailUrl = chefVM.MailUrl,
                LinkedinUrl = chefVM.LinkedinUrl
            };

            await _context.Chefs.AddAsync(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize("Admin,Moderator")]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Chef existed = await _context.Chefs.FirstOrDefaultAsync(c=> c.Id == id);
            if (existed != null)  return NotFound();

            UpdateChefVM chefVM = new UpdateChefVM
            {
                Image = existed.Image,
                Name = existed.Name,
                Position = existed.Position,
                FbUrl = existed.FbUrl,
                TwitterUrl = existed.TwitterUrl,
                MailUrl = existed.MailUrl,
                LinkedinUrl = existed.LinkedinUrl
            };

            return View(chefVM);
        }

        [Authorize("Admin,Moderator")]
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateChefVM chefVM)
        {
            if(!ModelState.IsValid) return View(chefVM);

            Chef existed = await _context.Chefs.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();

            bool result = _context.Chefs.Any(c=>c.Name == chefVM.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adli Chef artiq movcuddur");
                return View();
            }

            if(chefVM.Photo != null)
            {
                if (!chefVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uygun deyil");
                    return View(existed);
                }

                if (!chefVM.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olchusu uygun deyil");
                    return View(existed);
                }
                string newImage = await chefVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img", "chef");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "chef");
                existed.Image = newImage;
            }

            existed.Name = chefVM.Name;
            existed.Photo = chefVM.Photo;
            existed.Position = chefVM.Position;
            existed.FbUrl = chefVM.FbUrl;
            existed.TwitterUrl = chefVM.TwitterUrl;
            existed.MailUrl = chefVM.MailUrl;
            existed.LinkedinUrl = chefVM.LinkedinUrl;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0) return BadRequest();
            Chef chef = await _context.Chefs.FirstOrDefaultAsync(c => c.Id == id);
            if(chef==null) return NotFound();

            chef.Image.DeleteFile(_env.WebRootPath, "assets", "img", "chef");

            _context.Chefs.Remove(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details()
        {
            List<Chef> chefs = await _context.Chefs.ToListAsync();
            return View(chefs);
        }
    }
}
