using Mediator;
using Microsoft.AspNetCore.Mvc;
using SharedApplication.Users.GetUsers;
using SharedLogic.IdentityServer;

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
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AllUsers()
        {
            var userResult = await _mediator.Send(new GetSystemUsersQuery());
            return Ok(userResult);
        }

        [HttpPost]
        [Route("api/Users/Create")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Create()
        {
            return CreateProblemDetails(null);
        }
    }
}
