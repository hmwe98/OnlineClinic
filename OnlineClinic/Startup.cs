using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Data;
using OnlineClinic.Models;
using OnlineClinic.Services;

namespace OnlineClinic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IMail, GmailService>();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddSession(x => {
                x.Cookie.IsEssential = true;
                x.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            //services.AddSession(x =>
            //{
            //    x.Cookie.IsEssential = true;
            //    x.IdleTimeout = TimeSpan.FromMinutes(5);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            , UserManager<OnlineClinicUser> userManager,
            RoleManager<IdentityRole> roleManager,OnlineClinicDbHMU db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                       
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();            

            

            OCOnlineinitIdentityRoles(userManager, roleManager).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }                   
        private async Task OCOnlineinitIdentityRoles(UserManager<OnlineClinicUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {            
            if ((await roleManager.RoleExistsAsync("Admins")) == false)
            {
                var admin_role = new IdentityRole("Admins");
                await roleManager.CreateAsync(admin_role);
            }
            var adminuser = await userManager.FindByNameAsync("admin@gmail.com");
            if (adminuser == null)
            {
                adminuser = new OnlineClinicUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    name = "admin adminpoor",                    
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminuser, "Pp=-0987");
            }
            if (await userManager.IsInRoleAsync(adminuser, "Admins") == false)
            {
                await userManager.AddToRoleAsync(adminuser, "Admins");
            }
        }

    }
}
