using Dinner.Application.Common.Errors;
using Dinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;

namespace Dinner.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
     ErrorOr<AuthenticationResult> Register(
        string firstName,
        string lastName,
        string email,
        string password
     );
}
