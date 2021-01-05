using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab11.DataContext;
using Lab11.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Controllers
{
    public class UserController : Controller
    {
        private IDataContext _dataContext;

        public UserController(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View(_dataContext.GetUsers());
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View(_dataContext.GetUser(id));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dataContext.AddUser(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_dataContext.GetUser(id));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = id;
                    _dataContext.UpdateUser(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataContext.GetUser(id));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _dataContext.RemoveUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
