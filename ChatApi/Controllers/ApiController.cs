using ChatAPI.Application.Core;
using ChatAPI.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ChatApi.Controllers;

public class ApiController : ControllerBase
{

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

    public ActionResult<IEnumerable<TDTO>> ToActionResultEnumerable<TResult, TDTO>(StatusCode statusCode, Func<TResult, TDTO> toDTOFn, IEnumerable<TResult>? result, string validationMessage)
    {
        result ??= [];
        return ToActionResult(statusCode, (IEnumerable<TResult> results) => result.Select(toDTOFn), result, validationMessage);
    }
    public ActionResult<TDTO> ToActionResult<TResult, TDTO>(StatusCode statusCode, Func<TResult, TDTO> toDTOFn, TResult result, string validationMessage)
    {
        switch (statusCode)
        {
            case ChatAPI.Domain.Enums.StatusCode.Success:
                if (result == null)
                {
                    return new StatusCodeResult(500);
                }
                else
                {
                    return Ok(toDTOFn(result));
                }

            case ChatAPI.Domain.Enums.StatusCode.Error:
                return new StatusCodeResult(500);
            case ChatAPI.Domain.Enums.StatusCode.ValidationFailed:
                return ValidationProblem(validationMessage);
            case ChatAPI.Domain.Enums.StatusCode.NotAuthorized:
            case ChatAPI.Domain.Enums.StatusCode.NotAuthenticated:
            default:
                return Forbid();

        }
    }
}
