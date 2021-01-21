using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Lab12.Data;
using Lab12.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab12.Controllers
{

    public class ShopController : Controller
    {
        private readonly MyDbContext _context;
        private static int catId = -1;
        public ShopController(MyDbContext context)
        {
            _context = context;
        }
        // GET: Shop
        public IActionResult Index()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            SelectList list = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Articles"] = _context.Articles.Include(a => a.Category);

            return View();
        }

        public IActionResult ShowCart()
        {

            var req = Request.Cookies["cart"];
            Cart c = GetCart();
            var ctx = _context.Articles.Where(a => (c.Articles.Keys.ToList()).Contains(a.Id)).Include(a => a.Category);
            ViewData["Articles"] = ctx;
            ViewData["Quantity"] = c.Articles;
            ViewData["Count"] = ctx.Count() > 0;
            return View("Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter(int categoryID)
        {
            catId = categoryID;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Articles"] = getArticlesFromCategory(categoryID);

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);
            Cart c = GetCart();
            c.AddItem(article);

            SaveCartToCookie(c);
            return Filter(catId);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOneToCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);
            Cart c = GetCart();
            c.AddItem(article);

            SaveCartToCookie(c);
            return RedirectToAction("ShowCart");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubstractFromCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);
            Cart c = GetCart();
            c.RemoveOneItem(article);
            SaveCartToCookie(c);
            return RedirectToAction("ShowCart");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);

            Cart c = GetCart();
            c.RemoveItem(article);
            SaveCartToCookie(c);
            return RedirectToAction("ShowCart");

        }

        private Cart GetCart()
        {
            var req = Request.Cookies["cart"];
            Cart c = null;
            if (req != null)
            {
                c = JsonConvert.DeserializeObject<Cart>(req);
            }
            if (c == null)
            {
                c = new Cart();
            }
            return c;
        }
        private void SaveCartToCookie(Cart cart)
        {
            SetCookie("cart", JsonConvert.SerializeObject(cart), 7);
        }
        private IQueryable getArticlesFromCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return _context.Articles.Include(a => a.Category);
            }
            return _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == categoryId);
        }

        private void SetCookie(string key, string value, int? numberOfDays = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfDays.HasValue)
            {
                option.Expires = DateTime.Now.AddDays(numberOfDays.Value);
            }
            Response.Cookies.Append(key, value, option);
        }

        
    }

}
