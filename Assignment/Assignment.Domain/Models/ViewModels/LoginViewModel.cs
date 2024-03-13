using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب *")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "كلمه المرور مطلوبه *")]
        public string Password { get; set; }
    }
}
