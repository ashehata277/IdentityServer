using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Users.GetUsers
{
    public class GetSystemUsersQueryValidator : IValidationHandler<GetSystemUsersQuery>
    {
        public ValueTask<MediatorValidationResult> Validate(GetSystemUsersQuery request)
        {
            return ValueTask.FromResult(MediatorValidationResult.Success());
        }
    }
}
