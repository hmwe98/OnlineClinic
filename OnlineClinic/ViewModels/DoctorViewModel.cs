using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class DoctorViewModel
    {
        public string Id { get; set; }
        public string name { get; set; }        
        public string speciality { get; set; }        
        public string workexperience { get; set; }        
        public string Comunication_Email { get; set; }        
        public string BirthDate { get; set; }        
        public string MyFavorites { get; set; }        
        public IFormFile img{ get; set; }        
    }
}
