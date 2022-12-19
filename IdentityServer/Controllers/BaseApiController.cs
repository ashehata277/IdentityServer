using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        private readonly string titleId = "Api Error";
        private readonly string DefaultErrorMessage = "undefined Error";
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BadRequestObjectResult CreateProblemDetails(string? error)
        {
            var problemDetail = new ProblemDetails
            {
                Detail =error?? DefaultErrorMessage,
                Title= titleId,
                Type = typeof(string).ToString(),
                Instance = $"{_httpContextAccessor!.HttpContext!.Request.Scheme}://{_httpContextAccessor!.HttpContext!.Request.Host.Value}{_httpContextAccessor.HttpContext.Request.Path.Value}"
            };
            return BadRequest(problemDetail);
        }
    }
}
