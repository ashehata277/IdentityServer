using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseApiController : Controller
    {
        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        private const string TitleId = "Api Error";
        private const string DefaultErrorMessage = "undefined Error";
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BadRequestObjectResult CreateProblemDetails(string? error)
        {
            var problemDetail = new ProblemDetails
            {
                Detail = string.IsNullOrEmpty(error) ? DefaultErrorMessage : error,
                Title = TitleId,
                Type = typeof(string).ToString(),
                Instance =
                    $"{_httpContextAccessor!.HttpContext!.Request.Scheme}://{_httpContextAccessor!.HttpContext!.Request.Host.Value}{_httpContextAccessor.HttpContext.Request.Path.Value}",
            };
            return BadRequest(problemDetail);
        }

        protected IActionResult CreateApiResponse<T>(ResponseValidationWrapper<T> resultCqrs)
        {
            if (resultCqrs.IsSuccess) return Ok(resultCqrs.Response);
            else return CreateProblemDetails(resultCqrs.ErrorMessage);
        }
        
        protected IActionResult CreateApiResponse(ResponseValidationWrapper resultCqrs)
        {
            if (resultCqrs.IsSuccess) return Ok();
            else return CreateProblemDetails(resultCqrs.ErrorMessage);
        }
    }
}