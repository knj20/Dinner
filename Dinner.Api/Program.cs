using Dinner.Api.Filters;
using Dinner.Api.Middleware;
using Dinner.Application;
using Dinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


{
    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
}

var app = builder.Build();


{
    app.UseExceptionHandler("/error");
   // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
