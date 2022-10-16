using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Data;
using OnlineClinic.Models;
using OnlineClinic.Services;
using OnlineClinic.ViewModels;

namespace OnlineClinic.Controllers
{
    public class AccountController : Controller
    {
        OnlineClinicDbHMU db;        
        UserManager<OnlineClinicUser> userManager;
        SignInManager<OnlineClinicUser> SignInManager;
        public AccountController(OnlineClinicDbHMU _db,UserManager<OnlineClinicUser> _userManager, SignInManager<OnlineClinicUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            SignInManager = _signInManager;
        }

        public IActionResult Sign_up()
        {
            return View();
        }
        public IActionResult Sign_in()
        {
            return View();
        }        
        public async Task<IActionResult> RegisterConfirm(
            RegisterViewModel registerViewModel,[FromServices] IMail mail)
        {
            var id = Guid.NewGuid().ToString();
            OnlineClinicUser user = new OnlineClinicUser
            {
                Id = id,
                UserName = registerViewModel.username,
                name = registerViewModel.name,
                Email = registerViewModel.username,
                EmailConfirmed = false
            };     
            var status=await userManager.CreateAsync
                (user, registerViewModel.password);              
            if (status.Succeeded)
            {
                var realUser = await userManager.FindByIdAsync(id);
                var token = await userManager.GenerateEmailConfirmationTokenAsync
                    (realUser);
                realUser.confirmEmailtoken = token;
                await userManager.UpdateAsync(realUser);
                string address = Url.Action("RegisterConfirmLevelTwoBridge", "Account"
                    ,new {Id=user.Id,Token=token} 
                    ,"https");
                string body = $"سلام<b> {user.name}</b><br/>" 
                    + $"جهت فعال سازی حساب خود<a href='{address}'>لينك</a>" +
                    $" را كليك نماييد";
                string subject = "فعال سازی حساب کاربری";

                if (mail.SendEmail(user.Email, subject, body) == true)
                {
                    TempData["msg"] = "ايميل فعال سازی برای شما ارسال گردید";                                    
                }
                else
                {
                    TempData["msg"] = "خطا در ارسال فعال سازی";
                }            
            }
            else
            {
                    TempData["msg"] = "ايميل فعال سازی برای شما ارسال گردید";
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ActivateAccount(string username
            , [FromServices] IMail mail)
        {
            var realUser = await userManager
                    .FindByNameAsync(username);
            if (realUser != null)
            {
                if (realUser.EmailConfirmed == false)
                {
                    string address = Url.Action("RegisterConfirmLevelTwoBridge", "Account"
                        , new { Id = realUser.Id, Token = realUser.confirmEmailtoken }
                        , "https");
                    string body = $"سلام<b> {realUser.name}</b><br/>"
                        + $"جهت فعال سازی حساب خود<a href='{address}'>لينك</a>" +
                        $" را كليك نماييد";
                    string subject = "فعال سازی حساب کاربری";
                    if (mail.SendEmail(realUser.Email, subject, body) == true)
                    {
                        TempData["msg"] = "در صورت داشتن حساب كاربری در سایت ما ایمیل فعال سازی برای شما فرستاده شد";
                    }
                    else
                    {
                        TempData["msg"] = "خطا در ارسال ایمیل فعال سازی";
                    }
                }
                else
                {
                    TempData["msg"] = "در صورت داشتن حساب كاربری در سایت ما ایمیل فعال سازی برای شما فرستاده شد";
                }

            }
            else
            {
                TempData["msg"] = "در صورت داشتن حساب كاربری در سایت ما ایمیل فعال سازی برای شما فرستاده شد";
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RegisterConfirmLevelTwoBridge(string Id
            , string Token)
        {
            TempData["Token"] = Token;
            TempData["Id"] = Id;
            return RedirectToAction(nameof(RegisterConfirmLevelTwo));
        }
        public async Task<IActionResult> RegisterConfirmLevelTwo()            
        {
            string Id = TempData["Id"].ToString();
            string Token = TempData["Token"].ToString();
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {                                              
                var status = await userManager.ConfirmEmailAsync(user, Token);
                
                
                
                if (status.Succeeded)
                {
                    TempData["msg"] = "ثبت نام با موفقیت صورت گرفت";
                }
                else
                {
                    TempData["msg"] = "ثبت نام با موفقیت صورت نگرفت";
                }                
                return RedirectToAction("Index", "Home");                           
            }
            else
            {
                TempData["msg"] = "ثبت نام با موفقیت صورت نگرفت";
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> LoginConfirm(LoginViewModel loginViewModel
            , [FromServices] UserManager<OnlineClinicUser> userManager)
        {
            OnlineClinicUser user = await userManager.FindByNameAsync(loginViewModel.username);
            if (user != null)
            {
                var status = await SignInManager.PasswordSignInAsync(user, loginViewModel.password
                    , true
                    , true);
                if (status.Succeeded == true)
                {
                    TempData["msg"] = "با موفقيت وارد شديد";
                }
                else if (status.IsLockedOut)
                {
                    TempData["waitmsg"] = "حساب كاربری شما به دليل وارد كردن بيش از حد نام کاربری یا رمز عبور اشتباه مسدود شده است...در زمان ديگری تلاش بفرماييد";
                }
                if (status.Succeeded == false)
                {
                    TempData["msg"] = " نام کاربری یا رمز عبور خود را اشتباه وارد کردید";
                }                
            }
            else
            {
                TempData["msg"] = "نام کاربری یا رمز عبور خود را اشتباه وارد کردید";
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }  
        
        public async Task<IActionResult> ResetPasswordLevelOne(string username,
            [FromServices] IMail mail)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                string token = await userManager.GeneratePasswordResetTokenAsync(user);
                string address = Url.Action("ResetPasswordLevelTwoBridge", "Account"
                , new { id = user.Id, token = token }, "https");
                string body = $"سلام<b> {user.name}</b><br/>" + $"جهت تغيير رمز عبور<a href='{address}'>لينك</a> را كليك نماييد";
                string subject = "تغيير رمز عبور در كلينيك آنلاين";

                if (mail.SendEmail(user.Email, subject, body) == true)
                {
                    TempData["msg"] = "در صورت داشتن حساب كاربری در سایت ما ایمیلی جهت تغییر رمز عبور برای شما فرستاده شد";
                    user.token_resetpass_time = DateTime.Now;
                    await userManager.UpdateAsync(user);
                }
                else
                {
                    TempData["msg"] = "خطا در ارسال ایمیل تغییر رمز عبور";
                }

            }
            else
            {
                TempData["msg"] = "در صورت داشتن حساب كاربری در سایت ما ایمیلی جهت تغییر رمز عبور برای شما فرستاده شد";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ResetPasswordLevelTwoBridge(string id, string token)
        {
            TempData["id"] = id;
            TempData["token"] = token;
            return RedirectToAction(nameof(ResetPasswordLevelTwo));
        }
        public async Task<IActionResult> ResetPasswordLevelTwo()
        {
            string id = TempData["id"].ToString();
            string token = TempData["token"].ToString();
            var user = await userManager.FindByIdAsync(id);
            if (Math.Abs(DateTime.Now.Subtract(user.token_resetpass_time).TotalMinutes) >= 3)
            {
                TempData["msg"] = "مهلت زمانی برای تغییر رمز شما به پایان رسیده است...لطفا دوباره تلاش نمایید";
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.SetString("id", id);
            HttpContext.Session.SetString("token", token);            
            return View();
        }

        public async Task<IActionResult> ResetPasswordLevelThree(string newpassword)
        {
            string id = HttpContext.Session.GetString("id");
            string token = HttpContext.Session.GetString("token");
            HttpContext.Session.Clear();
            OnlineClinicUser user = await userManager.FindByIdAsync(id);
            var status = await userManager.ResetPasswordAsync(user, token,
            newpassword);
            if (status.Succeeded)
            {
                TempData["msg"] = "رمز با موفقيت عوض شد";
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
