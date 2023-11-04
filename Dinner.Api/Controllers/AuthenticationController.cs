using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Dinner.Application.Services.Authentication.Common;
using MediatR;
using Dinner.Application.Services.Authentication.Commands.Register;
using Dinner.Application.Services.Authentication.Queries.Login;

namespace Dinner.Api.Controllers;

[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var loginResult = await _mediator.Send(query);

        return loginResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));

    }
}
