using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "البريد الالكترونى مطلوب*")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب *")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "كلمه المرور مطلوبه *"), MinLength(6, ErrorMessage = "كلمه المرور قصيره اقل عدد من الحروف 3 *")]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
