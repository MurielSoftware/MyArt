using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Exceptions
{
    public class ValidationResult
    {
        public virtual string Message { get; set; }
        public virtual object[] Parameters { get; set; }

        public ValidationResult(string message, params object[] parameters)
        {
            Message = message;
            Parameters = parameters;
        }
    }
}
