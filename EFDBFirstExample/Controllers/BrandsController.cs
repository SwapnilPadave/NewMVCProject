using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDBFirstExample.Models;

namespace EFDBFirstExample.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brands
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CompanyDBContext db = new CompanyDBContext();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand b)
        {
            CompanyDBContext db = new CompanyDBContext();
            db.Brands.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index", "Brands");
        }
    }
}