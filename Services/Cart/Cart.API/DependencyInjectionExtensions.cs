using Cart.API.Data;
using Cart.API.Models;
using Carter;
using Marten;

namespace Cart.API;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddCarter();
        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("CartDb")!);
            options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
        }).UseLightweightSessions();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddScoped<ICartRepository,CartRepository>();
        
        return services;
    }
    public static WebApplication UseApplicationServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}
