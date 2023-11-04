using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Dinner.Application.Services.Authentication.Common;
using MediatR;
using Dinner.Application.Services.Authentication.Commands.Register;
using Dinner.Application.Services.Authentication.Queries.Login;
using MapsterMapper;

namespace Dinner.Api.Controllers;

[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

        return registerResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(request)),
            errors => Problem(errors));
       
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var loginResult = await _mediator.Send(query);

        return loginResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(request)),
            errors => Problem(errors));

    }
  
}
