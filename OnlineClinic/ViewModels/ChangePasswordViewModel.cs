using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "لطفا كلمه عبور فعلی را وارد كنيد")]
        public string currentpassword { get; set; }
        [Required(ErrorMessage = "لطفا كلمه عبور جدید را وارد كنيد")]
        public string newpassword { get; set; }
        [Required(ErrorMessage = "لطفا مجددا كلمه عبور جدید را وارد كنيد")]
        [Compare("newpassword", ErrorMessage = "رمز های عبور وارد شده یکسان نیستند")]
        public string repassword { get; set; }
    }
}
