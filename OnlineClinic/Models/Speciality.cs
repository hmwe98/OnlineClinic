using OnlineClinic.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string name { get; set; }
        public ICollection<Doctor> Doctors{ get; set; }
    }
}
