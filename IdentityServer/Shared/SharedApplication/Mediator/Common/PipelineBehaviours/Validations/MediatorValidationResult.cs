using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public record MediatorValidationResult
    {
        public string? ErrorMessage { get; set; }
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public static MediatorValidationResult Success(Object? result)
        {
            return new MediatorValidationResult { IsSuccess = true, Result = result };
        }
        public static MediatorValidationResult Success()
        {
            return new MediatorValidationResult { IsSuccess = true };
        }
        public static MediatorValidationResult Fail(string? errorMessage)
        {
            return new MediatorValidationResult { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
    public record MediatorValidationResult<T>
    {
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
        public bool IsSuccess { get; set; }

        public static MediatorValidationResult<T> Success(T? result)
        {
            return new MediatorValidationResult<T> { IsSuccess = true, Result = result };
        }
        public static MediatorValidationResult<T> Fail(string? errorMessage)
        {
            return new MediatorValidationResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
        }
        public static MediatorValidationResult<T> Success()
        {
            return new MediatorValidationResult<T> { IsSuccess = true };
        }

    }
}
