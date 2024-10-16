using MongoDB.Driver;

namespace API.ExtensionMethods;

public static class MongoDriverDiConfiguration
{
    public static void ConfigureMongoDriver(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
        serviceCollection.AddSingleton<IMongoClient>(new MongoClient(settings));
        
        var databaseName = configuration.GetSection("DatabaseName").Value;
        serviceCollection.AddScoped(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });
    }
}