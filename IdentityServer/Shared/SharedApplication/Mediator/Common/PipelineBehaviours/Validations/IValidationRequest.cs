using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public interface IValidationRequest :  IMessage
    {
    }
    public interface IValidationResponse 
    {
        public string? ErrorMessage { get; set; }
        public bool  IsSuccess{ get; set; }
    }
}
