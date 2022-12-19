using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public interface IValidationHandler
    {
        
    }


    public interface IValidationHandler<T> : IValidationHandler
    {
        ValueTask<MediatorValidationResult> Validate(T request);
    }
}
