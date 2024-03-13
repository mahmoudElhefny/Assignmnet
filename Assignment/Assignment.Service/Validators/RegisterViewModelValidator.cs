using Assignment.Service.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Validators
{
    public class RegisterViewModelValidator: AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(l => l)
                 .NotNull().WithMessage(("Can't found the object."));

            RuleFor(l => l.UserName)
                .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("UserName Can't Be Null")
              .NotEmpty().WithMessage("UserName Can't Be Empty");

            RuleFor(l => l.Password)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Password Can't Be Null")
               .Must(password =>
               {
                   if (string.IsNullOrEmpty(password))
                       return false;

                   // Check if the password contains alphabetic characters
                   bool hasAlphabeticChars = password.Any(char.IsLetter);

                   // Check if the password length is at least 8
                   bool hasMinimumLength = password.Length >= 8;

                   return hasAlphabeticChars && hasMinimumLength;
               })
            .WithMessage("Password must contain alphabetic characters and have a minimum length of 8.")

            .NotEmpty().WithMessage("Password Can't Be Empty");
            
            RuleFor(r => r.EmailAddress)
              .NotNull().WithMessage("Email Address can't be null")
             .NotEmpty().WithMessage("Email Address can't be empty")
             .EmailAddress().WithMessage("Invalid Email Address");
        }
    }
}
