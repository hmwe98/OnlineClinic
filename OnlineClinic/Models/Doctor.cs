using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string name{ get; set; }        
        public int specialityId { get; set; }
        [ForeignKey("specialityId")]
        public Speciality Speciality{ get; set; }
        public ICollection<Doctor_FreeTime> doctor_FreeTimes{ get; set; }
        public ICollection<Doctor_Patient> doctor_Patients{ get; set; }
    }
}
