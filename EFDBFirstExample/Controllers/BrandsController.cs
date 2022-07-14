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
        public ActionResult Index(string search="",int PageNo=1)
        {
            CompanyDBContext db = new CompanyDBContext();
            
            List<Brand> brands = db.Brands.Where(temp => temp.BrandName.Contains(search)).ToList();
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(brands.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (PageNo - 1) * NoOfRecordPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            brands = brands.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
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