using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string header { get; set; }
        public string shortstory { get; set; }
        public string content { get; set; }
        public byte[] img { get; set; }
    }
}
