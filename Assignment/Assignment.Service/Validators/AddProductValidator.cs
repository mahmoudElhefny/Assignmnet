using Assignment.Domain.ViewModels;
using Assignment.Infrastructure.__AppContext;
using Assignment.Service.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Validators
{
    public class AddProductValidator: AbstractValidator<productVM>
    {
        public AddProductValidator()
        {
            RuleFor(p => p)
                   .NotNull().WithMessage(("Can't found the object."));

            //RuleFor(p => p.ProductCode)
            //   // .Cascade(CascadeMode.Stop)
            //   .NotNull().WithMessage("ProductCode Can't Be Null")
            //   .Must(BeUniqueProductCode).WithMessage("ProductCode must be unique");
            // .NotEmpty().WithMessage("ProductCode Can't Be Empty");

            RuleFor(p => p.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Name Can't Be Null");
            //.NotEmpty().WithMessage("Name Can't Be Empty")
            //.EmailAddress().WithMessage("Name Formate Invalid");

            RuleFor(p => p.Price)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Price Can't Be Null");
            //.NotEmpty().WithMessage("Price Can't Be Empty");

            RuleFor(p => p.Image)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Image Can't Be Null");
               //.NotEmpty().WithMessage("Image Can't Be Empty");

            RuleFor(p=>p.Category_Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Category_Id Can't Be Null");

            RuleFor(p => p.DiscountRate)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("DiscountRate Can't Be Null");
               //.NotEmpty().WithMessage("DiscountRate Can't Be Empty"); 
        }

        //private bool BeUniqueProductCode(string arg)
        //{
        //    using (var dbContext = new ApplicationDbContext())
        //    {
        //        // Check if any user already has the given username
        //        bool isUnique = !dbContext.products.Any(u => u.ProductCode == arg);
        //        return isUnique;
        //    }
        //}
    }
}
