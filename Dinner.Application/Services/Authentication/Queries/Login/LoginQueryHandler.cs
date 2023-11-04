using Dinner.Application.Common.Interfaces.Authentication;
using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Application.Services.Authentication.Commands.Register;
using Dinner.Application.Services.Authentication.Common;
using Dinner.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Application.Services.Authentication.Queries.Login
{
   
    public class LoginQueryHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwTokenGenerator _jwTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwTokenGenerator jwTokenGenerator, IUserRepository userRepository)
        {
            _jwTokenGenerator = jwTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand query, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

            if (user.Password != query.Password)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }

            var token = _jwTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
