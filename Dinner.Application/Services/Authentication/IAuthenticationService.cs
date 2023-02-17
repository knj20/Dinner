using Dinner.Application.Common.Errors;
using FluentResults;

namespace Dinner.Application.Services.Authentication;

public interface IAuthenticationService
{
     AuthenticationResult Login(string email, string password);
    Result<AuthenticationResult> Register(
        string firstName,
        string lastName,
        string email,
        string password
    );
}
