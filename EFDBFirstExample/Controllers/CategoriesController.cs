using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDBFirstExample.Models;

namespace EFDBFirstExample.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Category> categories= db.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CompanyDBContext db = new CompanyDBContext();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category c)
        {
            CompanyDBContext db = new CompanyDBContext();
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
        
    }
}