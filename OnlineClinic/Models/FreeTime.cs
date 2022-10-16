using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class FreeTime
    {
        public int Id { get; set; }
        public string DateAndTime { get; set; }               
        public ICollection<Doctor_FreeTime> doctor_FreeTimes  { get; set; }
    }
}
