using Dinner.Application.Common.Errors;
using Dinner.Application.Common.Interfaces.Authentication;
using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Domain.Common.Errors;
using Dinner.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace Dinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwTokenGenerator _jwTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwTokenGenerator jwTokenGenerator, IUserRepository userRepository)
    {
        _jwTokenGenerator = jwTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            // throw new DuplicateEmailException();
            //throw new Exception("Email already existe");
            // return new DuplicateEmailError();
            // return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            return Errors.User.DuplicateEmail;
        }

        var user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        
        _userRepository.Add(user);

        var token = _jwTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if(user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
