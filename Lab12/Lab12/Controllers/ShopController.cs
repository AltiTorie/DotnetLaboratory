using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Controllers
{
    
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }
        // GET: Shop
        public async Task<IActionResult> Index()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            var myDbContext = _context.Articles.Include(a => a.Category);
            ViewData["Articles"] = myDbContext;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Filter(int categoryID)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            var myDbContext = _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == categoryID);
            //var myDbContext = from article in _context.Articles.Include(a => a.Category) where article.CategoryId == categoryID select article;
            ViewData["Articles"] = myDbContext;

            return View("Index");
        }
    }

}
