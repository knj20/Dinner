using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Dinner.Api.Filters;
using Dinner.Application.Common.Errors;
using ErrorOr;
using FluentResults;
using Dinner.Application.Services.Authentication.Queries;
using Dinner.Application.Services.Authentication.Common;
using Dinner.Application.Services.Authentication.Commands;

namespace Dinner.Api.Controllers;

[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationCommandService _AuthenticationCommandService;
    private readonly IAuthenticationQueryService _AuthenticationQueryService;

    public AuthenticationController(
        IAuthenticationCommandService authenticationCommandService,
        IAuthenticationQueryService authenticationQueryService)
    {
        _AuthenticationCommandService = authenticationCommandService;
        _AuthenticationQueryService = authenticationQueryService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult = _AuthenticationCommandService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
       
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                                    authResult.user.Id,
                                    authResult.user.FirstName,
                                    authResult.user.LastName,
                                    authResult.user.Email,
                                    authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var loginResult = _AuthenticationQueryService.Login(request.Email, request.Password);

        return loginResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));

    }
}
