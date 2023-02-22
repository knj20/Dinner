using Dinner.Application.Common.Errors;
using Dinner.Application.Common.Interfaces.Authentication;
using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Application.Services.Authentication.Common;
using Dinner.Domain.Common.Errors;
using Dinner.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace Dinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwTokenGenerator _jwTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwTokenGenerator jwTokenGenerator, IUserRepository userRepository)
    {
        _jwTokenGenerator = jwTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
