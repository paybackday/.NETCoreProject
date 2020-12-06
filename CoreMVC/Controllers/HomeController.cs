using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreMVC.Models.Context;
using CoreMVC.Models.Entities;
using CoreMVC.VMClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreMVC.Controllers
{
    public class HomeController : Controller
    {

        //CodeFirst icin EntityFramework Core KUTUPHANESINI Manage Nuget'ten indirmeyi unutmayin. Migration islemleri icin de Entity Framework Core Tools gerekmektedir.
        MyContext _db;
        public HomeController(MyContext db)
        {
            _db = db;
        }
        public IActionResult Login() {

            return View();
        }
        //.NET Core Authorization Islemleri
        //Async metotlar her zaman generic bir task dondurmek zorundadirlar. Siz isterseniz dondurulen bu taski kullanin isterseniz kullanmayin fakat dondurek zorundasinizdir.
        //Task Nedir? --> Task class i async metotlarin calisma prensipleri hakkinda ayrintili bilgiyi tutan (metot calisirke hata var mi, metot bu gorev sirasinda kendisine es zamanli gelen istekler, metodun calisma durumu(success,flawed)...) O yuzden normal sartlarda donen degeri task e generic olarak vermek zorundasiniz.

        [HttpPost]
        public async Task <IActionResult> Login(Employee employee) {
            Employee loginEmployee = _db.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName);
            if (loginEmployee!=null)
            {
                //Claim, rol bazli veya identity bazli guvenlik islemlerinden sorumlu olan bir class'tir.. Siz dilerseniz birden fazla Claim nesnesi yaratip hepsini ayni anda kullanabilirsiniz.

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,loginEmployee.UserRole.ToString())
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login"); // Burada login ismine sahip olan guvenlik durumu icin hangi guvenlik onlemlerinin calisacagini belirliyoruz.
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity); //.NET Core icerisinde sonlanmis olan security islemlerinin artik tetiklenmesi lazim (yani login isleminin yapilmasi lazim)

                //Asenkron metotlar calistiklari zaman baska bir islemin engellenmemesini saglayan metotlardir.
                //Eger siz bir asenkron metot kullaniyorsaniz mecburi bu metodu cagiran yapiya async keywordu vermeniz gerekiyor.
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Product");
            }
            return View(new EmployeeVM { Employee=employee});
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        
        }
    }
}
