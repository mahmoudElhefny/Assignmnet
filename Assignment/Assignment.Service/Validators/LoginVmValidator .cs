using Assignment.Service.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Validators
{
    public class LoginVmValidator: AbstractValidator<LoginViewModel>
    {
        public LoginVmValidator()
        {
            RuleFor(l => l)
                   .NotNull().WithMessage(("Can't found the object."));

            RuleFor(l => l.UserName)
              .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("UserName Can't Be Null")
            .NotEmpty().WithMessage("ProductCode Can't Be Empty");

            RuleFor(l => l.Password)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("UserName Can't Be Null")
            //   .Must(password =>
            //   {
            //       if (string.IsNullOrEmpty(password))
                //           return false;

                //       // Check if the password contains alphabetic characters
                //       bool hasAlphabeticChars = password.Any(char.IsLetter);

                //       // Check if the password length is at least 8
                //       bool hasMinimumLength = password.Length >= 8;

                //       return hasAlphabeticChars && hasMinimumLength;
                //   })
                //.WithMessage("Password must contain alphabetic characters and have a minimum length of 8.")
            .NotEmpty().WithMessage("Name Can't Be Empty")
            .EmailAddress().WithMessage("Name Formate Invalid");


        }

    }
}
