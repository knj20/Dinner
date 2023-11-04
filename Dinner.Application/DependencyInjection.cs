using Dinner.Application.Services.Authentication.Commands;
using Dinner.Application.Services.Authentication.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Dinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
