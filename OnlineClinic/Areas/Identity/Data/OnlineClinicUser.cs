using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineClinic.Models;

namespace OnlineClinic.Areas.Identity.Data
{
    public class OnlineClinicUser : IdentityUser
    {
        public ICollection<User_Visit> User_Visits{ get; set; }
        public string name { get; set; }        
        public string speciality { get; set; }
        public int nezamCode { get; set; }
        public string workexperience { get; set; }
        public string Comunication_Email { get; set; }
        public int age { get; set; }
        public string marital_status { get; set; }
        public string history { get; set; }
        public string medical_exam { get; set; }
        public string pastDeseases { get; set; }
        public string medications { get; set; }
        public string familyhistory { get; set; }
        public string treatment { get; set; }
        public DateTime token_resetpass_time { get; set; } 
        public string confirmEmailtoken { get; set; }
    }
}
