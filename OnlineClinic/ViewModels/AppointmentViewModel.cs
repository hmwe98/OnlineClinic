using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class AppointmentViewModel
    {
        public int tel { get; set; }
        public string gender { get; set; }
        public string doctor{ get; set; }
        public string speciality { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int time { get; set; }
    }
}
