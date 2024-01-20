using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;

namespace SharedApplication.Users.GetUsers
{
    public class GetSystemUsersQueryValidator : IValidationHandler<GetSystemUsersQuery>
    {
        public ValueTask<ValidationResult> Validate(GetSystemUsersQuery request)
        {
            return ValueTask.FromResult(ValidationResult.Success());
        }
    }
}
