using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Dinner.Application.Services.Authentication;
using Dinner.Api.Filters;
using Dinner.Application.Common.Errors;
using ErrorOr;
using FluentResults;

namespace Dinner.Api.Controllers;

[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _AuthenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _AuthenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult = _AuthenticationService.Register(
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
        var loginResult = _AuthenticationService.Login(request.Email, request.Password);

        return loginResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));

    }
}
