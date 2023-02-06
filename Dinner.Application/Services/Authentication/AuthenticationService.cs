using Dinner.Application.Common.Interfaces.Authentication;
using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Domain.Entities;

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
    AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User doesn't exist");
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

    AuthenticationResult IAuthenticationService.Login(string email, string password)
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
