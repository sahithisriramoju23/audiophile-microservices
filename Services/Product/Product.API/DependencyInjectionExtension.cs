using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Product.API.Data;

namespace Product.API;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ProductsDb")));
        services.AddScoped<ApplicationDbContext>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddCarter();
        return services;
    }
    public static WebApplication UseApplicationServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}
