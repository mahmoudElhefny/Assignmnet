using Assignment.Domain.ViewModels;
using Assignment.Service.ViewModels;
using FluentValidation;
using FluentValidation.Results;

namespace Assignment.Service.Validators
{
     class ValidatorHandler
    {
        public static List<Error> Validate<M>(M model, AbstractValidator<M> validator) where M : class
        {
            ValidationResult validationResult = validator.Validate(model);
            List<Error> Errors = null;
            if (!validationResult.IsValid)
            {
                Errors = ValidatorHandler.ErrorReturn(validationResult); ;
            }
            return Errors;
        }
        private static List<Error> ErrorReturn(ValidationResult validationResult)
        {
            List<Error> errors = new List<Error>();
            List<ValidationFailure> errores = new List<ValidationFailure>();
            foreach (var error in validationResult.Errors)
            {
                errores.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
            }
            errors = errores.Select(x => new Error() { ErrorMessage = x.ErrorMessage }).ToList();
            return errors;
        }
    }
}
