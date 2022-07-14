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
        CompanyDBContext db = new CompanyDBContext();
        // GET: Categories
        public ActionResult Index(string search = "",int PageNo=1)
        {
            ViewBag.search = search;
            List<Category> categories = db.Categories.Where(temp => temp.CategoryName.Contains(search)).ToList();
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(categories.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (PageNo - 1) * NoOfRecordPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            categories = categories.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category c)
        {           
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
        
        public ActionResult Update(long id)
        {
            Category exsistingCategory = db.Categories.Where(temp => temp.CategoryID == id).FirstOrDefault();
            return View(exsistingCategory);
        }
        [HttpPost]
        public ActionResult Update(Category c)
        {
            Category exsistingCategory = db.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            exsistingCategory.CategoryName = c.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
        
    }
}