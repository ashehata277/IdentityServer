using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedApplication.Users.GetUsers;
using SharedLogic.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Route("api/Users/All")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<User>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AllUsers()
        {
            var result = await _mediator.Send(new GetSystemUsersQuery());
            if (result.IsSuccess)
            {
                return Ok(result.Result);
            }
            else
            {
                return CreateProblemDetails(result.ErrorMessage);
            }
        }
    }
}
