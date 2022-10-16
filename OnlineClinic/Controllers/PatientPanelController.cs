using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Data;
using OnlineClinic.Models;
using OnlineClinic.ViewModels;

namespace OnlineClinic.Controllers
{
    [Authorize]
    public class PatientPanelController : Controller
    {
        OnlineClinicDbHMU db;
        UserManager<OnlineClinicUser> userManager;
        SignInManager<OnlineClinicUser> SignInManager;
        public PatientPanelController(OnlineClinicDbHMU _db, UserManager<OnlineClinicUser> _userManager, SignInManager<OnlineClinicUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            SignInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LastAppointments()
        {
            List <Visit> Visits = new List<Visit>();
            var patient = db.Users.Include(x => x.User_Visits).ThenInclude(x => x.visit)
                .FirstOrDefault(x => x.UserName == User.Identity.Name);
            patient.User_Visits.Reverse().ToList().ForEach(x =>
            {
                Visits.Add(x.visit);
            });
            ViewData["Visits"] = Visits;
            return View();
        }
        public IActionResult PatientAccountInfo()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public async Task<IActionResult> EditConfirm(PatientViewModel patientViewModel)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            user.name = patientViewModel.name;
            user.age = patientViewModel.age;
            db.SaveChanges();
            return RedirectToAction("EditProfile");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public async Task<IActionResult> ChangePasswordConfirm(ChangePasswordViewModel changePasswordViewModel)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var status = await userManager.ChangePasswordAsync(user, changePasswordViewModel.currentpassword
                , changePasswordViewModel.newpassword);
            if (status.Succeeded)
            {
                TempData["msg"] = "رمز با موفقيت عوض شد";
            }
            else
            {
                TempData["msg"] = "رمز با موفقيت عوض نشد";
            }
            return RedirectToAction("PatientAccountInfo");
        }
        public IActionResult FileDetail()
        {            
            return View();
        }
        public IActionResult AllPosts()
        {
            var Posts = db.BlogPosts.ToList();
            ViewData["Posts"] = Posts;
            return View();
        }
        public IActionResult Post(int Id)
        {
            var post = db.Find<BlogPost>(Id);
            ViewData["Post"] = post;
            return View();
        }
    }
}
