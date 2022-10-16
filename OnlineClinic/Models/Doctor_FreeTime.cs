using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClinic.Models
{
    public class Doctor_FreeTime
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        public int FreeTimeId { get; set; }
        [ForeignKey("FreeTimeId")]
        public FreeTime freeTime { get; set; }
        public int Count { get; set; }
        public bool status { get; set; }
    }
}