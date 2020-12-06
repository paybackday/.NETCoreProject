using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ConfigureServices' da hangi servisin konigure edilecegini soylersiniz. (Ancak henuz kullanamdiniz sadece servisleri ekliyorsunuz.)
            //Brada .NET Core adina standart bir Sql baglantisi belirtmek istiyorsaniz. (optionsBuilder'dan ziyade bu tercih edilir.) Bura belirlemelisiniz.

            //Pool kullanmak .NET Core da bir Singleton Pattern gorevi gorur.
            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"))); // Boylece baglanti ayarimizi burada belirlemis olduk.
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=Index}/{id?}");
            });
        }
    }
}
