using Dinner.Application.Common.Errors;
using Dinner.Application.Common.Interfaces.Authentication;
using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Domain.Entities;
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
    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            // throw new DuplicateEmailException();
            //throw new Exception("Email already existe");
            // return new DuplicateEmailError();
            return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
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

    public AuthenticationResult Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("user or password is not correct");
        }

        if(user.Password != password)
        {
            throw new Exception("user or password is not correct");
        }

        var token = _jwTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
