using Dinner.Application.Services.Authentication.Commands.Register;
using Dinner.Application.Services.Authentication.Common;
using Dinner.Application.Services.Authentication.Queries.Login;
using Dinner.Contracts.Authentication;
using Mapster;

namespace Dinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
              
        }
    }
}
