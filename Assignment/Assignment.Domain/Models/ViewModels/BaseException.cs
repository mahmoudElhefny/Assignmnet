using Assignment.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.ViewModels
{
    public class BaseException:Exception
    {
        public List<Error> Errors { get; set; }
        public string MoreDetails { get; set; }

        public BaseException()
        { }

        public BaseException(string message)
            : base(message)
        { }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
