using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Controllers;
using OnlineClinic.Models;

namespace OnlineClinic.Data
{
    public class OnlineClinicDbHMU : IdentityDbContext<OnlineClinicUser>
    {
        public DbSet<User_Visit> User_Visits{ get; set; }
        public DbSet<Visit>Visits{ get; set; }        
        public DbSet<Speciality>Specialities{ get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<FreeTime> FreeTimes { get; set; }
        public DbSet<Doctor_FreeTime> Doctor_FreeTimes { get; set; }
        public DbSet<Doctor_Patient> Doctor_Patients { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public OnlineClinicDbHMU(DbContextOptions<OnlineClinicDbHMU> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
