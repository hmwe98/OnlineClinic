using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class PostUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string header { get; set; }
        [Required]
        public string shortstory { get; set; }
        [Required]
        public string content { get; set; }
        public IFormFile img { get; set; }
    }
}
