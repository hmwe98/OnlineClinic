using OnlineClinic.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class User_Visit
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public OnlineClinicUser onlineClinicUser{ get; set; }
        public int VisitId { get; set; }
        [ForeignKey("VisitId")]
        public Visit visit{ get; set; }        
    }
}
