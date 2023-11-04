using Dinner.Api.Common.Mapping;

namespace Dinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
        services.AddControllers();

        services.AddMappings();
        return services;
    }
}
