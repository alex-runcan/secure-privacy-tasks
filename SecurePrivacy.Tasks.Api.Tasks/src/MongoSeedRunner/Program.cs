using DataAccess.Migrations;
using MongoDB.Driver;

namespace MongoSeedRunner;

class Program
{
    private const string ConnectionString = "mongodb://admin:adminpassword@localhost:27017/";
    private const string DatabaseName = "secure-privacy";
    
    static async Task Main(string[] args)
    {
        var client = new MongoClient(ConnectionString);
        var database = client.GetDatabase(DatabaseName);

        Console.WriteLine("Enter 'up' to run seed or 'down' to remove the seed items");

        while (true)
        {
            Console.Write(">> ");
            var input = Console.ReadLine();

            if (input.Equals("up", StringComparison.OrdinalIgnoreCase))
            {
                await database.UpAsync();
                Console.WriteLine("Up seed executed successfully.");
            }
            else if (input.Equals("down", StringComparison.OrdinalIgnoreCase))
            {
                await database.DownAsync();
                Console.WriteLine("Down seed executed successfully.");
            }
            else if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'up', 'down', or 'exit'.");
            }
        }
    }
}