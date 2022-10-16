using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام كاربری خود را وارد كنيد")]
        public string username { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد كنيد")]
        public string password { get; set; }
        public bool rememberme { get; set; }
    }
}
