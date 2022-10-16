using OnlineClinic.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public string patient_name{ get; set; }        
        public string patient_tel{ get; set; }
        public string patient_Id { get; set; }
        public string doctor_name{ get; set; }        
        public string DateAndTime { get; set; }
        public bool status { get; set; }             
        public ICollection<User_Visit> User_Visits { get; set; }
    }
}
