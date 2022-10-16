using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineClinic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Clinic_Intro() 
        {
            return View();
        }
        public IActionResult Floor_Guide() 
        {
            return View();
        }
        public IActionResult Doctor_Intro()
        {
            return View();
        }
        public IActionResult About_us()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult ObjectDetail()
        {
            return View();
        }
    }
}
