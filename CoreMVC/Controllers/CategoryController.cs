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
    public class CategoryController : Controller
    {
        //.NET Core da classlar ile calismaktan ziyade interfaceler ile calismaya yonelmislerdir.
        //Bu durumun sebebi SOLID Prensiplerinden Interface Segregation ve Dependency Inversion yapilarinin daha iyi uyum saglamasi icindir.
        //Interfaceler daha bagimsizdir.
        //Interfacelerin  ayri ayri gorevlerini tek cati altinda toplamaktansa sorumluluklarini biraz daha fazla ayirmaktan dolayidir.
        //Bakin bizde tek bir IRepository var ama IcategoryRepository acicaz vb. Sorumluluklari ayiracagiz.
        //Interfaceler birden fazla yere miras verebilir ve birden fazla yerden miras alabilir.
        //Geriye donus tipleri kumelenebilir.
        //Dependency Injection interfaceler ile daha kolay olur. Class lar ile yapilmaz daha zordur.
        //Dependency Injection istedigimiz an, istedigimiz sekilde, istedigimiz sorumlulugun hemen enjekte edilmesini saglayan bir tasarim paternidir.

        //Artik startup da yaptigimiz ayarlamalar sayesinde projemizin herhangi bir yerinde MyContext tipinde veya baska projelerde olabilecek olan veritabani sinifimiz kullanildigi anda otomatik olarak singletonpattern ayari gelecektir. Bunun sebebi MyContext isimli veritabani sinifimizin ctor unda options ayarinin bulunmasidir. Onun gittigi yer ise Startup da belirlenmistir.

        // **********.NET CORE'DA Viewlari tanitmak icin iceride bulunan web.config dosyasi yoktur. Onun yerine VMClass larin namespace leri ProjectX/Views/ViewImports.cshtml denen kisimda yazilir.**********


        MyContext _db;

        public CategoryController(MyContext db)
        {
            _db = db;
        }

        //.NET Core da MVC Helperlarin yerine daha performansli bir yapida bulunmaktadir. Bu yapinin adi TagHelper'lardir. 
        //Tag Helper: Normal HTML Tagler'inin icerisine yazilan attributelara verilen isimdir. Kullanabilmek icin namespace'leri gereklidir....
        public IActionResult Index()
        {
            CategoryVM cvm = new CategoryVM
            {
                Categories = _db.Categories.ToList()
            };
            return View(cvm);
        }
        public IActionResult AddCategory() {

            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category) // Eger bir VM yapinin icerisindeki property ismiyle buradaki parametre ismi tutuyorsa Bind a gerek yoktur.
        {
            _db.Categories.Add(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id) {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCategory(int id) {
            CategoryVM cvm = new CategoryVM
            {
                Category = _db.Categories.Find(id)
            };
            
            return View(cvm);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category) {
            _db.Entry(_db.Categories.Find(category.ID)).CurrentValues.SetValues(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
