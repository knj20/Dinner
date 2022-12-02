namespace Dinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    AuthenticationResult IAuthenticationService.Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "", "", email, "token");
    }

    AuthenticationResult IAuthenticationService.Register(
        string firstName,
        string lastName,
        string email,
        string password
    )
    {
        return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, "token");
    }
}
