using Dinner.Application.Common.Interfaces.Authentication;
using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Application.Services.Authentication.Common;
using Dinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Dinner.Application.Services.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwTokenGenerator _jwTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwTokenGenerator jwTokenGenerator, IUserRepository userRepository)
        {
            _jwTokenGenerator = jwTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                // throw new DuplicateEmailException();
                //throw new Exception("Email already existe");
                // return new DuplicateEmailError();
                // return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
                return Domain.Common.Errors.Errors.User.DuplicateEmail;
            }

            var user = new User()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.Add(user);

            var token = _jwTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
