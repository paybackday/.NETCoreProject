using CoreMVC.Configuration;
using CoreMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models.Context
{
    public class MyContext:DbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Bu bir konfigurasyin ayarlari yapildiginda isterseniz su tarz ayarlar yapabileceginiz yerdir. Modelin, veritabaninin konfigurasyon ayarlarini yapmaktir.
            
            EntityFrameCore.SqlServer kutuphanesini indirmeyi unutmayin. Options ayarlamalarini yapabilmek icin bu gerekecektir.

            Burda SqlServer kulanarak suraya baglan diyor...
            //optionsBuilder.UseSqlServer(connectionString: "server=.;database=CoreCodeFirst;uid=sa;pwd=1234");
        }*/

        //Startup.cs de gerekli pool ayarini yaptiktan sonra...
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        //Dependency Injection devreye girer. Su anda sadece CodeFirst icin nasil yapi kurulmasi gerektigi gosteriliyor. Dependency Injection yapisi Core platformuzunun arkasinda otomatik olarak entegre gelir. Dolayisiyla siz bir veritabani sinifinizin ctor unun parametre olarak bir options tipinde verirseniz. Bu parametreye arguman otomatik gonderilir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderDetail>().Property(x => x.ID).UseIdentityColumn(); Eger confg classimiz olmasaydi burada da tanimlayabilirdik fakat biz...
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
