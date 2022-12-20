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

        public static ResponseValidationWrapper<Tresponse> Success<Tresponse>(Tresponse response) 
        {
            return new ResponseValidationWrapper<Tresponse> { IsSuccess = true, Response = response };
        }
        public static ResponseValidationWrapper<Tresponse> Fail<Tresponse>(string errorMessage)
        {
            return new ResponseValidationWrapper<Tresponse> { IsSuccess = false, ErrorMessage = errorMessage};
        }
        public static ResponseValidationWrapper<Tresponse> Success<Tresponse>()
        {
            return new ResponseValidationWrapper<Tresponse> { IsSuccess = false};
        }

    }
}
