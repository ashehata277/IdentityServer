using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseApiController : Controller
    {
        public BaseApiController()
        {
        }

        private const string TitleId = "Api Error";
        private const string DefaultErrorMessage = "undefined Error";

        protected BadRequestObjectResult CreateProblemDetails(string? error)
        {
            var problemDetails = Results.Problem(statusCode: StatusCodes.Status400BadRequest,
                detail: error,
                extensions: new Dictionary<string, object?>()
                {

                });
            return BadRequest(problemDetails);
        }

        protected IActionResult CreateApiResponse<T>(ResponseValidationWrapper<T> resultCqrs)
        {
            if (resultCqrs.IsSuccess) return Ok(resultCqrs.Response);
            return CreateProblemDetails(resultCqrs.ErrorMessage);
        }

        protected IActionResult CreateApiResponse(ResponseValidationWrapper resultCqrs)
        {
            if (resultCqrs.IsSuccess) return Ok();
            else return CreateProblemDetails(resultCqrs.ErrorMessage);
        }
    }
}