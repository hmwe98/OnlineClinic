using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class PatientViewModel
    {
        public string Id { get; set; }
        public IFormFile img{ get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public int age{ get; set; }
        public string MyFavorites{ get; set; }
        public string BirthDate{ get; set; }
    }
}
