using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Areas.Account.Models;
using MovieApp.Web.Areas.BackOffice.Models;
using MovieApp.Web.Models;


namespace MovieApp.Web
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
            services.AddDbContext<ApplicationRegisterModel>(options => options.UseSqlServer(Configuration.GetConnectionString("Myconnection")));
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            }

            );

            
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
            app.UseSession();
            app.UseEndpoints (endpoints =>
            {

                endpoints.MapControllerRoute(null, "Account/Login", new { area = "Account", controller = "Account", action = "Login" });
                endpoints.MapControllerRoute(null, "Account/Register", new { area = "Account", controller = "Account", action = "Register" });

                endpoints.MapControllerRoute(null, "BackOffice/Films", new { area = "BackOffice", controller = "Films", action = "Index" });
                endpoints.MapControllerRoute(null, "BackOffice/Films/Create", new { area = "BackOffice", controller = "Films", action = "Create"  });
                endpoints.MapControllerRoute(null, "BackOffice/Films/Edit", new { area = "BackOffice", controller = "Films", action = "Edit" });
                endpoints.MapControllerRoute(null, "BackOffice/Films/Delete", new { area = "BackOffice", controller = "Films", action = "Delete" });
                endpoints.MapControllerRoute(null, "BackOffice/Films/Details", new { area = "BackOffice", controller = "Films", action = "Details" });

                endpoints.MapControllerRoute(null, "BackOffice/Serials", new { area = "BackOffice", controller = "Serials", action = "Index" });
                endpoints.MapControllerRoute(null, "BackOffice/Serials/Create", new { area = "BackOffice", controller = "Serials", action = "Create" });
                endpoints.MapControllerRoute(null, "BackOffice/Serials/Edit", new { area = "BackOffice", controller = "Serials", action = "Edit" });
                endpoints.MapControllerRoute(null, "BackOffice/Serials/Delete", new { area = "BackOffice", controller = "Serials", action = "Delete" });
                endpoints.MapControllerRoute(null, "BackOffice/Serials/Details", new { area = "BackOffice", controller = "Serials", action = "Details" });

                endpoints.MapControllerRoute(null, "BackOffice/FavFilms", new { area = "BackOffice", controller = "FavFilms", action = "Index" });
                endpoints.MapControllerRoute(null, "BackOffice/FavFilms/Create", new { area = "BackOffice", controller = "FavFilms", action = "Create" });
                endpoints.MapControllerRoute(null, "BackOffice/FavFilms/Edit", new { area = "BackOffice", controller = "FavFilms", action = "Edit" });
                endpoints.MapControllerRoute(null, "BackOffice/FavFilms/Delete", new { area = "BackOffice", controller = "FavFilms", action = "Delete" });
                endpoints.MapControllerRoute(null, "BackOffice/FavFilms/Details", new { area = "BackOffice", controller = "FavFilms", action = "Details" });

                endpoints.MapControllerRoute(null, "BackOffice/FavSerials", new { area = "BackOffice", controller = "FavSerials", action = "Index" });
                endpoints.MapControllerRoute(null, "BackOffice/FavSerials/Create", new { area = "BackOffice", controller = "FavSerials", action = "Create" });
                endpoints.MapControllerRoute(null, "BackOffice/FavSerials/Edit", new { area = "BackOffice", controller = "FavSerials", action = "Edit" });
                endpoints.MapControllerRoute(null, "BackOffice/FavSerials/Delete", new { area = "BackOffice", controller = "FavSerials", action = "Delete" });
                endpoints.MapControllerRoute(null, "BackOffice/FavSerials/Details", new { area = "BackOffice", controller = "FavSerials", action = "Details" });

                endpoints.MapControllerRoute(null, "BackOffice/ComingSoon", new { area = "BackOffice", controller = "comingsoon", action = "Index" });
                endpoints.MapControllerRoute(null, "BackOffice/ComingSoon/Create", new { area = "BackOffice", controller = "comingsoon", action = "Create" });
                endpoints.MapControllerRoute(null, "BackOffice/ComingSoon/Edit", new { area = "BackOffice", controller = "comingsoon", action = "Edit" });
                endpoints.MapControllerRoute(null, "BackOffice/ComingSoon/Delete", new { area = "BackOffice", controller = "comingsoon", action = "Delete" });
                endpoints.MapControllerRoute(null, "BackOffice/ComingSoon/Details", new { area = "BackOffice", controller = "comingsoon", action = "Details" });

                endpoints.MapControllerRoute(null, "BackOffice/Watchlists", new { area = "BackOffice", controller = "Watchlists", action = "Index" });
                endpoints.MapControllerRoute(null, "BackOffice/Watchlists/Create", new { area = "BackOffice", controller = "Watchlists", action = "Create" });
                endpoints.MapControllerRoute(null, "BackOffice/Watchlists/Edit", new { area = "BackOffice", controller = "Watchlists", action = "Edit" });
                endpoints.MapControllerRoute(null, "BackOffice/Watchlists/Delete", new { area = "BackOffice", controller = "Watchlists", action = "Delete" });
                endpoints.MapControllerRoute(null, "BackOffice/Watchlists/Details", new { area = "BackOffice", controller = "Watchlists", action = "Details" });
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
