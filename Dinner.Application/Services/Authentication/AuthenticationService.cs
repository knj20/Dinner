using Dinner.Application.Common.Interfaces.Authentication;

namespace Dinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwTokenGenerator _jwTokenGenerator;

    public AuthenticationService(IJwTokenGenerator jwTokenGenerator)
    {
        _jwTokenGenerator = jwTokenGenerator;
    }
    AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
    {
        Guid userId = Guid.NewGuid();
        var token = _jwTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, token);
    }

    AuthenticationResult IAuthenticationService.Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "", "", email, "token");
    }
}
