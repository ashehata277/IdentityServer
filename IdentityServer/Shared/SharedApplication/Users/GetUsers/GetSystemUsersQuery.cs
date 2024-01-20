using Mediator;
using Microsoft.EntityFrameworkCore;
using SharedApplication.ContextInterfaces;
using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;
using SharedLogic.IdentityServer;

namespace SharedApplication.Users.GetUsers
{
    public record GetSystemUsersQuery : IRequest<ResponseValidationWrapper<List<User>>>, IValidationRequest
    {
        public class GetSystemUsersQueryHandler : IRequestHandler<GetSystemUsersQuery, ResponseValidationWrapper<List<User>>>
        {
            private readonly IIDentityContext _identityContext;

            public GetSystemUsersQueryHandler(IIDentityContext identityContext)
            {
                this._identityContext = identityContext;
            }
            public async ValueTask<ResponseValidationWrapper<List<User>>> Handle(GetSystemUsersQuery request, CancellationToken cancellationToken)
            {
                var allUsers = await _identityContext.User.ToListAsync(cancellationToken: cancellationToken);
                return ResponseValidationWrapper<List<User>>.Success(allUsers);
            }
        }
    }
}
