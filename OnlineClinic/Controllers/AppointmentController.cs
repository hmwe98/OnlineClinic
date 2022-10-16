//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Data;
using OnlineClinic.Models;

namespace OnlineClinic.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        OnlineClinicDbHMU db;        
        UserManager<OnlineClinicUser> userManager;
        SignInManager<OnlineClinicUser> SignInManager;
        public AppointmentController(OnlineClinicDbHMU _db, UserManager<OnlineClinicUser> _userManager, SignInManager<OnlineClinicUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            SignInManager = _signInManager;
        }
        public IActionResult GetAppointment()
        {            
            var specialities = db.Specialities.ToList();
            ViewData["specialities"] = specialities;
            return View();
        }
        public List<string> SearchDoctor(int Id)
        {
            List<string> Doctornames = new List<string>();
            var specialities = db.Specialities.Include(x => x.Doctors).Where(x => x.Id == Id);
            specialities.ToList().ForEach(x =>
            {
                x.Doctors.ToList().ForEach(x =>
                {
                    Doctornames.Add(x.name);
                });
            });
            return Doctornames;
        }
        public List<string> TimeSearch(string doctor)
        {            
            List<string> FreeTimes = new List<string>();
            var doctors = db.Doctors.Include(x => x.doctor_FreeTimes).ThenInclude(x => x.freeTime)
                .Where(x => x.name == doctor);            
            doctors.ToList().ForEach(x =>
            {
                x.doctor_FreeTimes.ToList().ForEach(x =>
                {
                    if (x.status == true)
                    {
                        FreeTimes.Add(x.freeTime.DateAndTime);                                       
                    }
                });
            });
            return FreeTimes;            
        }
        public async Task<IActionResult> AppointmentConfirm(string speciality,string doctor,string DAT,string tel)
        {
            if (User.IsInRole("Doctors")==false)
            {
                if (speciality == null || doctor == null || DAT == null || tel == null)
                {
                    return RedirectToAction("GetAppointment");
                }                
                var externaldoctor = db.Doctors.Include(x=>x.doctor_Patients).ThenInclude(x=>x.patient).
                    FirstOrDefault(x => x.name == doctor);                
                var freetime = db.FreeTimes.FirstOrDefault(x => x.DateAndTime == DAT);
                var doctor_freetime = db.Doctor_FreeTimes.FirstOrDefault(x => x.FreeTimeId == freetime.Id &&
                  x.DoctorId == externaldoctor.Id);            
                if (doctor_freetime.Count < 5)
                {                
                    var Doctor = db.Users.FirstOrDefault(x => x.name == doctor);
                    var Patient = await userManager.FindByNameAsync(User.Identity.Name);
                    Patient.PhoneNumber = tel;
                    var searchvisit = db.Visits.FirstOrDefault(x => x.DateAndTime == DAT && x.doctor_name == doctor
                      && x.patient_name == Patient.name);
                    Visit visit = new Visit()
                    {
                        patient_tel = tel,
                        patient_name = Patient.name,
                        doctor_name = doctor,
                        patient_Id=Patient.Id,
                        DateAndTime = DAT,
                        status = false,
                    };
                    if(searchvisit == null)
                    {                    
                        User_Visit patient_Visit = new User_Visit()
                        {
                            visit = visit,                                               
                            onlineClinicUser = Patient
                        };
                        db.Add(patient_Visit);
                        Patient.User_Visits.Add(patient_Visit);
                        doctor_freetime.Count++;
                        db.SaveChanges();                    
                    }
                    var search2visit = db.Visits.FirstOrDefault(x => x.DateAndTime == DAT && x.doctor_name == doctor
                      && x.patient_name == Patient.name);                
                    var NotIterateDoctor = db.User_Visits.FirstOrDefault(x => x.UserId == Doctor.Id && x.VisitId == search2visit.Id);
                    if (NotIterateDoctor == null)
                    {
                        User_Visit doctor_Visit = new User_Visit()
                        {
                            visit = visit,                                               
                            onlineClinicUser = Doctor
                        };
                        db.Add(doctor_Visit);
                        Doctor.User_Visits.Add(doctor_Visit);
                        db.SaveChanges();
                    }
                }
                if(doctor_freetime.Count==5)
                {
                    doctor_freetime.status = false;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("GetAppointment");
        }
    }
}
