using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClinic.Models
{
    public class Doctor_Patient
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor doctor { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient patient{ get; set; }
    }
}