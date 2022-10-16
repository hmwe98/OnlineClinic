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
    [Authorize(Policy= "DoctorPolicy")]
    public class DoctorPanelController : Controller
    {
        OnlineClinicDbHMU db;
        UserManager<OnlineClinicUser> userManager;
        SignInManager<OnlineClinicUser> SignInManager;
        public DoctorPanelController(OnlineClinicDbHMU _db, UserManager<OnlineClinicUser> _userManager, SignInManager<OnlineClinicUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            SignInManager = _signInManager;
        }
        public IActionResult FutureVisits()
        {
            List<Visit> FutureVisits = new List<Visit>();                                   
            var user = db.Users.Include(x=>x.User_Visits).ThenInclude(x=>x.visit).FirstOrDefault(x=>x.UserName==User.Identity.Name);            
            user.User_Visits.Reverse().Where(x=>x.visit.status==false).Take(5).ToList().ForEach(x =>
            {                
                FutureVisits.Add(x.visit);                                               
            });
            ViewData["FutureVisits"] = FutureVisits;
            return View();
        }        
        public IActionResult DoctorAccountInfo()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
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
            if(status.Succeeded)
            {
                TempData["msg"] = "رمز با موفقيت عوض شد";
            }
            else
            {
                TempData["msg"] = "رمز با موفقيت عوض نشد";
            }            
            return RedirectToAction("DoctorAccountInfo");
        }
        public async Task<IActionResult> EditConfirm(DoctorViewModel doctorViewModel)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            user.name = doctorViewModel.name;
            user.speciality = doctorViewModel.speciality;
            user.workexperience = doctorViewModel.workexperience;
            user.Comunication_Email = doctorViewModel.Comunication_Email;            
            db.SaveChanges();
            return RedirectToAction("EditProfile");
        }
        public IActionResult PatientInfo(string Id)
        {
            var patient = db.Users.FirstOrDefault(x => x.Id == Id);
            ViewData["Patient"] = patient;
            return View();
        }
        public IActionResult AddOrEditPatient(string Id,string DAT)
        {
            var patientuser = db.Users.FirstOrDefault(x=>x.Id==Id);                        
            ViewData["Patient"] = patientuser;
            ViewData["DAT"] = DAT;
            return View();
        }
        public IActionResult EditPatientConfirm(MedicalFileViewModel medicalFileViewModel)
        {
            var doctoruser1 = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var patientuser = db.Users.Include(x=>x.User_Visits).ThenInclude(x=>x.visit)
                .FirstOrDefault(x => x.Id == medicalFileViewModel.patientId);
            var a = patientuser.User_Visits;
            patientuser.User_Visits.ToList().ForEach(x =>
            {
                if (x.visit.DateAndTime == medicalFileViewModel.DAT 
                && x.visit.doctor_name==doctoruser1.name)
                {
                    x.visit.status = true;
                }
            });
            patientuser.age = medicalFileViewModel.age;
            patientuser.marital_status = medicalFileViewModel.maritalstatus;
            patientuser.familyhistory = medicalFileViewModel.familyhistory;
            patientuser.history = medicalFileViewModel.history;
            patientuser.medical_exam = medicalFileViewModel.medical_exam;
            patientuser.treatment = medicalFileViewModel.treatment;
            patientuser.pastDeseases = medicalFileViewModel.pastdeseases;
            patientuser.medications = medicalFileViewModel.medication;
            var searchpatient = db.Patients.FirstOrDefault(x => x.userId == patientuser.Id);
            if (searchpatient == null)
            {
                var doctoruser2 = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);                
                var externaldoctor = db.Doctors.Include(x => x.doctor_Patients).ThenInclude(x => x.patient).
                    FirstOrDefault(x => x.name == doctoruser2.name);
                Patient patient = new Patient()
                {
                    userId = patientuser.Id
                };
                db.Add(patient);
                Doctor_Patient doctor_Patient = new Doctor_Patient()
                {
                    doctor = externaldoctor,
                    patient = patient,
                };
                db.Add(doctor_Patient);
                externaldoctor.doctor_Patients.Add(doctor_Patient);
            }
            db.SaveChanges();
            return RedirectToAction("DoctorAccountInfo");
        }
        public IActionResult ShowAllVisits(int number)
        {
            int count = 0;
            List<Visit> FutureVisits = new List<Visit>();
            var user = db.Users.Include(x => x.User_Visits).ThenInclude(x => x.visit)
                .FirstOrDefault(x => x.UserName == User.Identity.Name);
            count = user.User_Visits.Where(x => x.visit.status == false).Count();            
            user.User_Visits.Reverse().Where(x=>x.visit.status==false).Skip((number-1)*1).Take(1).ToList().ForEach(x =>
            {                                
                FutureVisits.Add(x.visit);                
            });//            
            ViewData["FutureVisits"] = FutureVisits;
            ViewBag.backnumber = number-1;
            ViewBag.nextnumber = number+1;
            if (count % 1 == 0)//
            {
                ViewBag.count = (count / 1);//
            }
            else
            {
                ViewBag.count = (count/1)+1;//
            }
            return View();
        }        
        public IActionResult PatientSearch(string searchname)
        {
            if (searchname == null)
            {
                return View();
            }
            List<Visit> Visits = new List<Visit>();
            var user = db.Users.Include(x => x.User_Visits).ThenInclude(x => x.visit)
                .FirstOrDefault(x => x.UserName == User.Identity.Name);
            user.User_Visits.Reverse().ToList().ForEach(x =>
            {
                if (x.visit.patient_name.Replace(" ","").Contains(searchname.Replace(" ", "")))
                {
                    Visits.Add(x.visit);
                }
            });
            ViewData["Visits"] = Visits;
            return View();            
        }
        public IActionResult GiveAppointment()
        {            
            return View();
        }
        public IActionResult GiveAppointmentConfirm(GiveAppointmentViewModel giveAppointmentViewModel)
        {
            if( giveAppointmentViewModel.Date == null || giveAppointmentViewModel.Time == null)
            {
                return RedirectToAction("GiveAppointment");
            }
            var date = giveAppointmentViewModel.Date;
            var time = giveAppointmentViewModel.Time;
            var dateandtime = time + " " + date;
            var searchfreetime = db.FreeTimes.FirstOrDefault(x => x.DateAndTime == dateandtime);
            if (searchfreetime == null)
            {
                FreeTime freeTime = new FreeTime()
                {
                    DateAndTime = dateandtime
                };
                var doctor = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                var externaldoctor = db.Doctors.Include(x => x.doctor_FreeTimes).ThenInclude(x => x.freeTime)
                    .Include(x => x.doctor_FreeTimes).ThenInclude(x => x.Doctor)
                    .FirstOrDefault(x => x.name == doctor.name);
                Doctor_FreeTime doctor_FreeTime = new Doctor_FreeTime()
                {
                    Doctor=externaldoctor,
                    freeTime=freeTime,
                    status=true,
                    Count=0
                };
                db.Add(freeTime);
                db.Add(doctor_FreeTime);
                externaldoctor.doctor_FreeTimes.Add(doctor_FreeTime);                                        
                db.SaveChanges();
            }
            return RedirectToAction("AppointmentTimes");
        }
        public IActionResult AppointmentTimes()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var DoctorFreeTimes = db.Doctors.Include(x=>x.doctor_FreeTimes).ThenInclude(x=>x.freeTime)
                .FirstOrDefault(x => x.name == user.name).doctor_FreeTimes.ToList();
            ViewData["FreeTimes"] = DoctorFreeTimes;
            return View();
        }
        public IActionResult RemoveTime(int Id)
        {
            var doctor_freetime = db.Doctor_FreeTimes.Include(x=>x.freeTime).Include(x=>x.Doctor)
                .FirstOrDefault(x=>x.Id==Id);
            var freetime = db.FreeTimes.FirstOrDefault
                (x => x.DateAndTime == doctor_freetime.freeTime.DateAndTime);
            db.Remove(freetime);                        
            db.SaveChanges();
            return RedirectToAction("AppointmentTimes");
        }
        public IActionResult EditTime(int Id)
        {
            HttpContext.Session.SetInt32("timeId", Id);
            return View();
        }    
        public IActionResult EditTimeConfirm(GiveAppointmentViewModel giveAppointmentViewModel)
        {
            if (giveAppointmentViewModel.Date == null || giveAppointmentViewModel.Time == null)
            {
                return RedirectToAction("EditTime");
            }
            var Id = HttpContext.Session.GetInt32("tiemId");
            HttpContext.Session.Remove("timeId");
            var doctor_freetime = db.Doctor_FreeTimes.Include(x => x.freeTime)
                .FirstOrDefault(x => x.Id == Id);            
            var freetime = db.FreeTimes.FirstOrDefault
                (x => x.DateAndTime == doctor_freetime.freeTime.DateAndTime);
            var date = giveAppointmentViewModel.Date;
            var time = giveAppointmentViewModel.Time;
            var dateandtime = time + " " + date;            
            freetime.DateAndTime = dateandtime; 
            db.SaveChanges();
            return RedirectToAction("AppointmentTimes");
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
