using DataAccess.AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DiConfiguration
{
    public static void ConfigureDataAccessDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DataAccessProfile));
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}