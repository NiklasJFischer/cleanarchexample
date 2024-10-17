using ChatApi.Core;
using ChatApi.Entities;
using ChatApi.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;

namespace ChatApi.Controllers
{
    public class ApiController : ControllerBase
    {
        protected void AddLogToConsole(Log log)
        {
            ArgumentNullException.ThrowIfNull(log, nameof(log));

            Debug.WriteLine("");
            Debug.WriteLine(log.Timestamp);
            Debug.WriteLine(log.Title);
            Debug.WriteLine(log.Description);
            Debug.WriteLine("");
        }

        public UserContext UserContext
        {
            get
            {
                UserContext userContext = new()
                {
                    UserId = Guid.Empty
                };

                var idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (idClaim == null)
                {
                    return userContext;
                }

                var userIdString = idClaim.Value;

                if (userIdString.IsNullOrEmpty())
                {
                    return userContext;
                }

                userContext.UserId = Guid.Parse(userIdString);
                return userContext;
            }
        }

        public ActionResult<IEnumerable<TDTO>> ToActionResultEnumerable<TResult, TDTO>(StatusCode statusCode, Func<TResult, TDTO> toDTOFn, IEnumerable<TResult> result, string validationMessage)
        {
            return ToActionResult(statusCode, (IEnumerable<TResult> results) => result.Select(toDTOFn), result, validationMessage);
        }
        public ActionResult<TDTO> ToActionResult<TResult, TDTO>(StatusCode statusCode, Func<TResult, TDTO> toDTOFn, TResult result, string validationMessage)
        {
            switch (statusCode)
            {
                case Enums.StatusCode.Success:
                    if (result == null)
                    {
                        return new StatusCodeResult(500);
                    }
                    else
                    {
                        return Ok(toDTOFn(result));
                    }

                case Enums.StatusCode.Error:
                    return new StatusCodeResult(500);
                case Enums.StatusCode.ValidationFailed:
                    return ValidationProblem(validationMessage);
                case Enums.StatusCode.NotAuthorized:
                case Enums.StatusCode.NotAuthenticated:
                default:
                    return Forbid();

            }
        }
    }
}
