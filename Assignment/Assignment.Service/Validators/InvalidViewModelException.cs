using Assignment.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Validators
{
    public class InvalidViewModelException: BaseException
    {
        
        public InvalidViewModelException(List<Error> errors)
        {
            Errors = errors;
        }
    }
}
