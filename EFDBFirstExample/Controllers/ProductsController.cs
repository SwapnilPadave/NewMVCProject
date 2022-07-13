using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EFDBFirstExample.Models;
using QRCoder;

namespace EFDBFirstExample.Controllers
{
    public class ProductsController : Controller
    {
        CompanyDBContext db = new CompanyDBContext();
        // GET: Products
        public ActionResult Index(string search="",string SortColumn="ProductName",string IconClass="fa-sort-asc",int PageNo=1)
        {            
            //List<Product> products = db.Products.Where(temp=>temp.CategoryID==1).ToList();
            ViewBag.search = search;
            List<Product> products =db.Products.Where(temp=>temp.ProductName.Contains(search)).ToList();
            ViewBag.Sortcolumn = SortColumn;
            ViewBag.IconClass = IconClass;
            //for Product ID
            if (ViewBag.SortColumn == "ProductID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.ProductID).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.ProductID).ToList();
                }
            }
            //For product Name
           else if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.ProductName).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
                }
            }
            //For Price
            else if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.Price).ToList();
                }
            }
            //For Date Of Purchase
            else if (ViewBag.SortColumn == "DateOfPurchase")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.DateOfPurchase).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.DateOfPurchase).ToList();
                }
            }
            //For Availability status
            else if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.AvailabilityStatus).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.AvailabilityStatus).ToList();
                }
            }
            //For Category
            else if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.Category.CategoryName).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.Category.CategoryName).ToList();
                }
            }
            //for Brand
            else if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(temp => temp.Brand.BrandName).ToList();
                }
                else
                {
                    products = products.OrderByDescending(temp => temp.Brand.BrandName).ToList();
                }
            }
            //Paging
            int NoOfRecordPerPage = 3;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (PageNo - 1) * NoOfRecordPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(products);
        }
        public ActionResult Details(int id)
        {           
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }
        //Products/Create
        [HttpGet]
        public ActionResult Create()
        {           
            ViewBag.categories = db.Categories.ToList();
            ViewBag.brands = db.Brands.ToList();
           return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {            
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new Byte[file.ContentLength];
                file.InputStream.Read(imgBytes, 0, file.ContentLength);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                p.Photo = base64String;
            }
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long id)
        {            
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.brands = db.Brands.ToList();
            return View(existingProduct);
        }
        [HttpPost]
        public async Task< ActionResult> Edit(Product p)
        {                        
            Product existingProduct = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
            existingProduct.ProductName = p.ProductName;
            existingProduct.Price = p.Price;
            existingProduct.AvailabilityStatus = p.AvailabilityStatus;
            existingProduct.DateOfPurchase = p.DateOfPurchase;
            existingProduct.CategoryID = p.CategoryID;
            existingProduct.BrandID = p.BrandID;
            existingProduct.Active = p.Active;
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Products");
        }
        public ActionResult Delete(long id)
        {            
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(existingProduct);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(long id,Product p)
        {
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(existingProduct);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Products");
        }        
        
        public ActionResult Booking(long id)
        {
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.brands = db.Brands.ToList();
            ViewBag.products = db.Products.ToList();
            Random r = new Random();
            int number = r.Next(10, 1000000);
            Product p = new Product();            
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("Your Booking No Is:-" + number.ToString() + " " + "Product ID:-" + existingProduct.ProductID + " " +"Product Name:-" + existingProduct.ProductName +
                                                                 " " + "Price:-" + existingProduct.Price + " " + "Category ID:-" + existingProduct.Category.CategoryName + " " + "Brand ID:-" + existingProduct.Brand.BrandName,
                                                                 QRCodeGenerator.ECCLevel.Q);            
            QRCode qRCode = new QRCode(qRCodeData);            
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = qRCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    ViewBag.BookingId = number.ToString();                                           
                }
            }
            return View();
        }
    }
}