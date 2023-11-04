using Dinner.Api;
using Dinner.Application;
using Dinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


{
    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration)
                    .AddPresentation();

}

var app = builder.Build();


{
    app.UseExceptionHandler("/error");
   // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
