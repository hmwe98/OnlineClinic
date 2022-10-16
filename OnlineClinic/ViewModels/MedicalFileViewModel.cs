using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class MedicalFileViewModel
    {        
        public string patientId { get; set; }
        public string DAT { get; set; }
        [Required(ErrorMessage = "سن را وارد نمایید")]        
        public int age { get; set; }
        [Required(ErrorMessage = "وضعیت تاهل را وارد نمایید")]
        public string maritalstatus { get; set; }
        [Required(ErrorMessage = "شرح حال بیمار را وارد نمایید")]
        public string history { get; set; }
        [Required(ErrorMessage = "شرح معاینه بیمار را وارد نمایید")]
        public string medical_exam { get; set; }
        [Required(ErrorMessage = "سابقه بیماری را وارد نمایید")]
        public string pastdeseases { get; set; }
        [Required(ErrorMessage = "داروهای مصرفی را وارد نمایید")]
        public string medication { get; set; }
        [Required(ErrorMessage = "سابقه بیماری خانوادگی را وارد نمایید")]
        public string familyhistory { get; set; }
        [Required(ErrorMessage = "پلن درمانی را وارد نمایید")]
        public string treatment { get; set; }
    }
}
