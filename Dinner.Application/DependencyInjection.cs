using Dinner.Application.Common.Behaviors;
using Dinner.Application.Services.Authentication.Commands;
using Dinner.Application.Services.Authentication.Commands.Register;
using Dinner.Application.Services.Authentication.Common;
using Dinner.Application.Services.Authentication.Queries;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        //services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
