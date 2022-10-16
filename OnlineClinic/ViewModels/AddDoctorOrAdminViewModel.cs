using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.ViewModels
{
    public class AddDoctorOrAdminViewModel
    {
        [Required(ErrorMessage ="تخصص را وارد نمایید")]
        public int SpecialityId { get; set; }
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد كنيد")]
        public string name { get; set; }
        [Required(ErrorMessage = "لطفا نام كاربری خود را وارد كنيد")]
        [EmailAddress(ErrorMessage = "ايميل شما نامعتبر است")]
        public string username { get; set; }
        [Required(ErrorMessage = "لطفا كلمه عبور خود را وارد كنيد")]
        public string password{ get; set; }
        [Required(ErrorMessage = "لطفا مجددا كلمه عبور خود را وارد كنيد")]
        [Compare("password", ErrorMessage = "رمز های عبور وارد شده یکسان نیستند")]
        public string repassword{ get; set; }
    }
}
