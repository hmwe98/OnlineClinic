using System;
using System.Collections.Generic;
using System.Linq;
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

    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        OnlineClinicDbHMU db;
        UserManager<OnlineClinicUser> userManager;
        public AdminController(OnlineClinicDbHMU _db,UserManager<OnlineClinicUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAllUsers()
        {
            return View();
        }        
        public async Task<IActionResult> AddUserToRole(string id, string rolename,
            [FromServices] UserManager<OnlineClinicUser> userManager)
        {
            OnlineClinicUser user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, rolename);
            return Json(true);
        }
        public async Task<IActionResult> RemoveUserFromRole(string id, string rolename,
            [FromServices] UserManager<OnlineClinicUser> userManager)
        {
            OnlineClinicUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, rolename);
            return Json(true);
        }
        public IActionResult SearchUser(string name)
        {
            if (name == null)
            {
                return RedirectToAction("ShowAllUsers");
            }
            List<OnlineClinicUser> users = new List<OnlineClinicUser>();
            db.Users.ToList().ForEach(x =>
            {
                if (x.name.Replace(" ", "").Contains(name.Replace(" ", "")))
                {
                    users.Add(x);
                }
            });            
            ViewData["users"] = users;
            return View();
        }
        public IActionResult AddAdmin()
        {
            return View();
        }
        public async Task<IActionResult> AddAdminConfirm(AddDoctorOrAdminViewModel addAdminViewModel)
        {
            OnlineClinicUser user = new OnlineClinicUser
            {
                UserName = addAdminViewModel.username,
                name = addAdminViewModel.name,
                Email = addAdminViewModel.username,
                EmailConfirmed = true
            };
            var status = await userManager.CreateAsync(user, addAdminViewModel.password);
            if (status.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admins");
                TempData["msg"] = "با موفقيت ثبت شد";                
            }
            else
            {
                TempData["msg"] = "موفقيت آميز نبود";
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult AddSpeciality()
        {
            return View();
        }
        public IActionResult AddSpecialityConfirm(string speciality)
        {
            var specialitysearch = db.Specialities.Where(x => x.name == speciality).FirstOrDefault();
            if (specialitysearch == null)
            {
                TempData["msg"] = "با موفقيت ثبت شد";
                Speciality speciality1 = new Speciality()
                {
                    name=speciality
                };
                db.Add(speciality1);
                db.SaveChanges();
            }
            else
            {
                TempData["msg"] = "موفقیت آمیز نبود";
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult AddDoctor()
        {
            ViewData["SpecialityList"] = db.Specialities.ToList();
            return View();
        }
        public async Task<IActionResult> AddDoctorConfirm(AddDoctorOrAdminViewModel addDoctorViewModel)
        {
            if (addDoctorViewModel.SpecialityId == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            OnlineClinicUser user = new OnlineClinicUser
            {
                UserName = addDoctorViewModel.username,
                name = addDoctorViewModel.name,
                Email = addDoctorViewModel.username,
                EmailConfirmed = true
            };
            var status = await userManager.CreateAsync(user, addDoctorViewModel.password);
            if (status.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Doctors");
                TempData["msg"] = "با موفقيت ثبت شد";
                Doctor doctor = new Doctor()
                {
                    name=addDoctorViewModel.name,               
                };
                var specialitysearch = db.Specialities.Include(x=>x.Doctors)
                    .Where(x=>x.Id==addDoctorViewModel.SpecialityId).FirstOrDefault();
                specialitysearch.Doctors.Add(doctor);
                db.SaveChanges();
            }
            else
            {
                TempData["msg"] = "موفقيت آميز نبود";
            }
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult ShowDoctors()
        {
            ViewData["Doctors"]= db.Doctors.Include(x=>x.Speciality).ToList();
            return View();
        }
        public async Task<IActionResult> ShowAdmins()
        {
            var admins = await userManager.GetUsersInRoleAsync("Admins");
            ViewData["admins"] = admins;
            return View();
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
            return RedirectToAction("Index");
        }
        public IActionResult RemoveUser(string user)
        {
            var User = db.Find<OnlineClinicUser>(user);
            db.Users.Remove(User);
            db.SaveChanges();
            return RedirectToAction("ShowAllUsers");
        }
        public IActionResult AddPost1()
        {
            return View();
        }
        public IActionResult PostConfirm1(PostViewModel postViewModel)
        {
            BlogPost blogPost = new BlogPost()
            {
                header=postViewModel.header, 
                shortstory=postViewModel.shortstory
            };
            if (postViewModel.img != null)
            {
                if (postViewModel.img.Length <= (5 * Math.Pow(1024, 2)))
                {
                    if (System.IO.Path.GetExtension
                   (postViewModel.img.FileName.ToLower()) == ".jpeg"
                   || System.IO.Path.GetExtension
                   (postViewModel.img.FileName.ToLower()) == ".jpg" ||
                   System.IO.Path.GetExtension
                   (postViewModel.img.FileName.ToLower()) == ".png")
                    {
                        byte[] b = new byte[postViewModel.img.Length];
                        postViewModel.img.OpenReadStream().Read(b, 0, b.Length);
                        blogPost.img = b;
                    }
                }
            }
            else
            {
                return RedirectToAction("AddPost1");
            }
            string StringBlogpost = Newtonsoft.Json.JsonConvert
                .SerializeObject(blogPost);
            HttpContext.Session.SetString("initpost", StringBlogpost);            
            return RedirectToAction("AddPost2");
        }
        public IActionResult AddPost2()
        {
            return View();
        }
        public IActionResult PostConfirm2(string htmlcode)
        {
            var StringBlogpost = HttpContext.Session.GetString("initpost");            
            HttpContext.Session.Remove("initpost");
            BlogPost initblogPost = Newtonsoft.Json.JsonConvert
                .DeserializeObject<BlogPost>(StringBlogpost);
            BlogPost blogPost = new BlogPost()
            {
                header=initblogPost.header,
                img=initblogPost.img,
                shortstory=initblogPost.shortstory,
                content=htmlcode
            };
            db.Add(blogPost);
            db.SaveChanges();
            return RedirectToAction("AddPost1");
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
        public IActionResult EditPost1(int Id)
        {
            var post = db.Find<BlogPost>(Id);
            ViewData["Post"] = post;
            return View();
        }
        public IActionResult PostEditConfirm1(PostUpdateViewModel postViewModel)
        {            
            var post = db.Find<BlogPost>(postViewModel.Id);
            post.header = postViewModel.header;
            post.shortstory = postViewModel.shortstory;
            if(postViewModel.img != null)
            {
                if (postViewModel.img.Length <= (5 * Math.Pow(1024, 2)))
                {
                    if (System.IO.Path.GetExtension
                   (postViewModel.img.FileName.ToLower()) == ".jpeg"
                   || System.IO.Path.GetExtension
                   (postViewModel.img.FileName.ToLower()) == ".jpg" ||
                   System.IO.Path.GetExtension
                   (postViewModel.img.FileName.ToLower()) == ".png")
                    {
                        byte[] b = new byte[postViewModel.img.Length];
                        postViewModel.img.OpenReadStream().Read(b, 0, b.Length);
                        post.img = b;
                    }
                }
            }
            db.SaveChanges();            
            HttpContext.Session.SetInt32("postId", postViewModel.Id);
            return RedirectToAction("EditPost2");
        } 
        public IActionResult EditPost2()
        {
            var Id= HttpContext.Session.GetInt32("postId");
            HttpContext.Session.Remove("postId");
            var post = db.Find<BlogPost>(Id);
            ViewData["Post"] = post;
            return View();
        }
        public IActionResult PostEditConfirm2(PostUpdateViewModel postViewModel)
        {
            var post = db.Find<BlogPost>(postViewModel.Id);
            post.content = postViewModel.content;
            db.SaveChanges();
            return RedirectToAction("AllPosts");
        }                
        public IActionResult RemovePost(int Id)
        {
            db.Remove(db.Find<BlogPost>(Id));
            db.SaveChanges();
            return RedirectToAction("AllPosts");
        }

    }
}
