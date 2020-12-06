using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models.Context;
using CoreMVC.Models.Entities;
using CoreMVC.VMClasses;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class ProductController : Controller
    {
        //Ben route icerisinde www.localhost.com/Product/.... yazdigimda Product i gordugu an instance alinir ve ctor calisir.
        MyContext _db;
        public ProductController(MyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ProductVM pvm = new ProductVM()
            {
                Products = _db.Products.ToList(),
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }
        public IActionResult AddProduct() {
            ProductVM pvm = new ProductVM()
            {
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int id) {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateProduct(int id) {
            ProductVM pvm = new ProductVM
            {
                Product=_db.Products.Find(id),
                Categories=_db.Categories.ToList()
            };
            return View(pvm);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product) {
            _db.Entry(_db.Products.Find(product.ID)).CurrentValues.SetValues(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
