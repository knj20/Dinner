using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Dinner.Application.Services.Authentication;

namespace Dinner.Api.Controllers;

[ApiController]
[Route("auth")]
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
        var registerResult = _AuthenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse(
            registerResult.user.Id,
            registerResult.user.FirstName,
            registerResult.user.LastName,
            registerResult.user.Email,
            registerResult.Token
        );
        return Ok(response);
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
