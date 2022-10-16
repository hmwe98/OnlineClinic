using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Data;

[assembly: HostingStartup(typeof(OnlineClinic.Areas.Identity.IdentityHostingStartup))]
namespace OnlineClinic.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OnlineClinicDbHMU>(options =>
                    options.UseSqlServer(
                context.Configuration.GetConnectionString("OnlineClinicDbHMUConnection")));

                services.AddDefaultIdentity<OnlineClinicUser>
                (options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<OnlineClinicDbHMU>();
                services.Configure<IdentityOptions>(x =>
                {
                    
                    x.Lockout.AllowedForNewUsers = true;
                    x.Lockout.MaxFailedAccessAttempts = 3;
                    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                    x.Password.RequiredLength = 7;
                    x.Password.RequireDigit = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    //x.Password.RequiredUniqueChars = 1;
                });
                services.ConfigureApplicationCookie(x =>
                {
                    x.LoginPath = "/Account/Sign_in";
                    x.AccessDeniedPath = "/Account/Sign_in";
                });
                services.AddAuthorization(x =>
                {
                    x.AddPolicy("DoctorPolicy"
                    , p => p.RequireRole("Doctors"));                                       
                });
                services.AddAuthorization(y =>
                {
                    y.AddPolicy("AdminPolicy"
                    , p => p.RequireRole("Admins"));                                       
                });
            });
        }
    }
}