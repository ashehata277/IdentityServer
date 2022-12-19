using Mediator;
using Microsoft.EntityFrameworkCore;
using SharedApplication.ContextInterfaces;
using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;
using SharedLogic.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Users.GetUsers
{
    public record GetSystemUsersQuery : IRequest<MediatorValidationResult>
    {
        public class GetSystemUsersQueryHandler : IRequestHandler<GetSystemUsersQuery, MediatorValidationResult>
        {
            private readonly IIDentityContext _iDentityContext;

            public GetSystemUsersQueryHandler(IIDentityContext iDentityContext)
            {
                this._iDentityContext = iDentityContext;
            }
            public async ValueTask<MediatorValidationResult> Handle(GetSystemUsersQuery request, CancellationToken cancellationToken)
            {
                var allUsers = await _iDentityContext.User.ToListAsync();
                return MediatorValidationResult.Success(allUsers);
            }
        }
    }
}
