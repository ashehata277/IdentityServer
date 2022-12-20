using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public class ResponseValidationWrapper<T> : IValidationResponse 
    {
        public string? ErrorMessage { get; set; } = null;
        public bool IsSuccess { get ; set ; }
        public T? Response{ get; set; }

        public static ResponseValidationWrapper<T> Success(T response) 
        {
            return new ResponseValidationWrapper<T> { IsSuccess = true, Response = response };
        }
        public static ResponseValidationWrapper<T> Fail(string errorMessage)
        {
            return new ResponseValidationWrapper<T> { IsSuccess = false, ErrorMessage = errorMessage};
        }
        public static ResponseValidationWrapper<T> Success()
        {
            return new ResponseValidationWrapper<T> { IsSuccess = false};
        }

    }
}
