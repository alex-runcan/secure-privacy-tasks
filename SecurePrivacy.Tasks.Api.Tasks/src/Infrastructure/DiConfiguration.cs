using Domain.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DiConfiguration
{
    public static void ConfigureInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}