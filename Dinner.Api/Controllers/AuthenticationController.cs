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
            registerResult.Id,
            registerResult.FirstName,
            registerResult.LastName,
            registerResult.Email,
            registerResult.Token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var loginResult = _AuthenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            loginResult.Id,
            loginResult.FirstName,
            loginResult.LastName,
            loginResult.Email,
            loginResult.Token
        );
        return Ok(response);
    }
}
