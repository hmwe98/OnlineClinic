using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string userId{ get; set; }
        public ICollection<Doctor_Patient> doctor_Patients{ get; set; }
    }
}
