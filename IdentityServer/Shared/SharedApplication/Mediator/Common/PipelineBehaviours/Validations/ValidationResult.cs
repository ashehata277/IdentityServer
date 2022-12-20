using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public record ValidationResult
    {
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public static ValidationResult Success()
        {
            return new ValidationResult { IsSuccess = true };
        }
        public static ValidationResult Fail(string? errorMessage)
        {
            return new ValidationResult { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
   
}
