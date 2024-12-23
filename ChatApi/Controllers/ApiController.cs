﻿using ChatAPI.Application.Commands.Core;
using ChatAPI.Presenters.Core;
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

    public ActionResult<IEnumerable<TViewModel>> PresentEnumerable<TResult, TViewModel>(CommandResult<IEnumerable<TResult>> serviceResult, IPresenter<TViewModel, TResult> presenter)
    {
        return Present(serviceResult, new EnumerablePresenter<TViewModel, TResult>(presenter));
    }

    public ActionResult<TViewModel> Present<TResult, TViewModel>(CommandResult<TResult> serviceResult, IPresenter<TViewModel, TResult> presenter)
    {
        switch (serviceResult.StatusCode)
        {
            case CommandResultStatusCode.Success:
                if (serviceResult.Result == null)
                {
                    return new StatusCodeResult(500);
                }
                else
                {
                    return Ok(presenter.Present(serviceResult.Result));
                }

            case CommandResultStatusCode.Error:
                return new StatusCodeResult(500);
            case CommandResultStatusCode.ValidationFailed:
                return ValidationProblem(serviceResult.Message);
            case CommandResultStatusCode.NotAuthorized:
            case CommandResultStatusCode.NotAuthenticated:
            default:
                return Forbid();

        }
    }
}
