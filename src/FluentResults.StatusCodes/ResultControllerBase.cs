using System;
using FluentResults.StatusCodes.Errors;
using FluentResults.StatusCodes.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FluentResults.StatusCodes
{
    public class ResultControllerBase : ControllerBase
    {
        [NonAction]
        public ActionResult ValidateResult(Result result, Func<ActionResult> success = null, Action failure = null)
        {
            if (!result.IsFailed)
            {
                return success?.Invoke();
            }

            failure?.Invoke();

            var errorMessages = result.GetErrorsMessages();

            if (result.HasError<InvalidInputError>())
            {
                return BadRequest(new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Title = "Bad Request",
                    Status = 400,
                    Type = "https://httpstatuses.com/400",
                    Detail = errorMessages
                });
            }

            if (result.HasError<UnauthorizedError>())
            {
                return StatusCode(403, new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Title = "Forbidden",
                    Status = 403,
                    Type = "https://httpstatuses.com/403",
                    Detail = errorMessages
                });
            }

            if (result.HasError<NotFoundError>())
            {
                return NotFound(new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Title = "Not Found",
                    Status = 404,
                    Type = "https://httpstatuses.com/404",
                    Detail = errorMessages
                });
            }

            if (result.HasError<BusinessValidationError>())
            {
                return Conflict(new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Title = "Conflict",
                    Status = 409,
                    Type = "https://httpstatuses.com/409",
                    Detail = errorMessages
                });
            }

            return StatusCode(500, new ProblemDetails
            {
                Instance = HttpContext.Request.Path,
                Title = "Internal Server Error",
                Status = 500,
                Type = "https://httpstatuses.com/500",
                Detail = errorMessages
            });
        }
    }
}