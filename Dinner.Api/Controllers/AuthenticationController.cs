using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Dinner.Application.Services.Authentication;
using Dinner.Api.Filters;
using Dinner.Application.Common.Errors;
using FluentResults;

namespace Dinner.Api.Controllers;

[ApiController]
[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _AuthenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _AuthenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        Result<AuthenticationResult> registerResult = _AuthenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        if(registerResult.IsSuccess)
        {
            return Ok(MapAuthResult(registerResult.Value));
        }
        var firstError = registerResult.Errors[0];

        if(firstError is DuplicateEmailError)
        {
            return Problem(statusCode: StatusCodes.Status409Conflict, detail:"email already exists");
        }

        return Problem();
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

        var response = new AuthenticationResponse(
            loginResult.user.Id,
            loginResult.user.FirstName,
            loginResult.user.LastName,
            loginResult.user.Email,
            loginResult.Token
        );
        return Ok(response);
    }
}
