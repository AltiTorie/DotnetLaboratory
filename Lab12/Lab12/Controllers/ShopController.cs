using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
            Cart c = null;
            if (req != null)
            {
                c = JsonConvert.DeserializeObject<Cart>(req);
            }
            if (c == null)
            {
                c = new Cart();
            }
            var ctx = _context.Articles.Where(a => (c.Articles.Keys.ToList()).Contains(a.Id));
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
            ViewData["Articles"] = getArticles(categoryID);

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);
            Cart c = TryGetCart();
            c.AddItem(article);

            SaveCartToCookie(c);
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            //ViewData["Articles"] = getArticles(catId);
            return Filter(catId);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOneToCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);
            Cart c = TryGetCart();
            c.AddItem(article);

            SaveCartToCookie(c);
            //var ctx = _context.Articles.Where(a => (c.Articles.Keys.ToList()).Contains(a.Id));
            //ViewData["Articles"] = ctx;
            //ViewData["Quantity"] = c.Articles;
            //ViewData["Count"] = ctx.Count() > 0;
            return RedirectToAction("ShowCart");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubstractFromCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);
            Cart c = TryGetCart();
            c.RemoveOneItem(article);
            SaveCartToCookie(c);
            //var ctx = _context.Articles.Where(a => (c.Articles.Keys.ToList()).Contains(a.Id));
            //ViewData["Articles"] = ctx;
            //ViewData["Quantity"] = c.Articles;
            //ViewData["Count"] = ctx.Count() > 0;
            return RedirectToAction("ShowCart");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromCart(int articleId)
        {
            ViewBag.context = _context;
            var article = _context.Articles.Find(articleId);

            Cart c = TryGetCart();
            c.RemoveItem(article);
            SaveCartToCookie(c);
            //var ctx = _context.Articles.Where(a => (c.Articles.Keys.ToList()).Contains(a.Id));
            //ViewData["Articles"] = ctx;
            //ViewData["Quantity"] = c.Articles;
            //ViewData["Count"] = ctx.Count() > 0;
            return RedirectToAction("ShowCart");

        }

        private Cart TryGetCart()
        {
            var req = Request.Cookies["cart"];
            Cart c = null;
            if (req != null)
            {
                c = JsonConvert.DeserializeObject<Cart>(req);
            }
            //Cart c = TryGetCart();
            if (c == null)
            {
                c = new Cart();
            }
            return c;
        }
        private void SaveCartToCookie(Cart cart)
        {
            SetCookie("cart", JsonConvert.SerializeObject(cart), 604800);
        }
        private IQueryable getArticles(int categoryId)
        {
            if (categoryId <= 0)
            {
                return _context.Articles.Include(a => a.Category);
            }
            return _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == categoryId);
        }

        private void SetCookie(string key, string value, int? numberOfSeconds = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfSeconds.HasValue)
            {
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            }
            Response.Cookies.Append(key, value, option);
        }

        private class Cart
        {
            public Dictionary<int, int> Articles { get; private set; }

            public Cart()
            {
                Articles = new Dictionary<int, int>();
            }

            public void AddItem(Article article)
            {
                if (Articles.ContainsKey(article.Id))
                {
                    Articles[article.Id] = Articles[article.Id] + 1;
                }
                else
                {
                    Articles.Add(article.Id, 1);
                }
            }

            public void RemoveOneItem(Article article)
            {
                if (Articles[article.Id] > 1)
                {
                    Articles[article.Id] = Articles[article.Id] - 1;
                }
                else
                {
                    Articles.Remove(article.Id);
                }
            }

            public void RemoveItem(Article article)
            {
                if (Articles.ContainsKey(article.Id))
                {
                    Articles.Remove(article.Id);
                }
            }
        }
    }

}
