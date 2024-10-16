using DataAccess.Entities;
using MongoDB.Driver;

namespace DataAccess.Migrations;

public static class M001_AddInitialIndexes
{

    public static async Task UpAsync(this IMongoDatabase db)
    {
        var productsCollection = db.GetCollection<ProductEntity>(ProductEntity.CollectionName);
        await CreateUniqueNameIndexAsync(productsCollection);
        await CreateCompoundProductIndexAsync(productsCollection);
    }

    public static async Task DownAsync(this IMongoDatabase db)
    {
        var productsCollection = db.GetCollection<ProductEntity>(ProductEntity.CollectionName);
        await RemoveUniqueNameIndexAsync(productsCollection);
        await RemoveCompoundProductIndexAsync(productsCollection);
    }

    private static async Task CreateUniqueNameIndexAsync(IMongoCollection<ProductEntity> productsCollection)
    {
        var indexName = "Unique_Name";
        var uniqueNameIndexAlreadyExist = productsCollection.Indexes.List().ToList()
            .Any(index => index["name"] == indexName && index["unique"] == true);
        if (uniqueNameIndexAlreadyExist)
        {
            Console.WriteLine($"Index {indexName} already exists, delete it first");
            return;
        }
        var uniqueNameIndexKeys = Builders<ProductEntity>.IndexKeys.Ascending(x => x.Name);
        CreateIndexOptions uniqueNameIndexOptions = new ()
        {
            Unique = true,
            Name = indexName
        };
        CreateIndexModel<ProductEntity> uniqueNameIndexModel = new (uniqueNameIndexKeys, uniqueNameIndexOptions);
        
        await productsCollection.Indexes.CreateOneAsync(uniqueNameIndexModel);
        Console.WriteLine($"Index {indexName} was successfully created!");
    }

    private static async Task RemoveUniqueNameIndexAsync(IMongoCollection<ProductEntity> productsCollection)
    {
        var indexName = "Unique_Name";
        var indexExist = productsCollection.Indexes.List().ToList()
            .Any(index => index["name"] == indexName && index["unique"] == true);

        if (!indexExist)
        {
            Console.WriteLine($"Index {indexName} does not exist!");
            return;
        }
        
        await productsCollection.Indexes.DropOneAsync("Unique_Name");
        Console.WriteLine($"Index {indexName} has been dropped!");
    }

    private static async Task CreateCompoundProductIndexAsync(IMongoCollection<ProductEntity> productsCollection)
    {
        var compoundKeysDefinition = Builders<ProductEntity>.IndexKeys
            .Ascending(p => p.Rating)
            .Ascending(p => p.Price);
        var compoundIndexOptions = new CreateIndexOptions { Name = "Rating_Price_Index" };
        var compoundIndexModel = new CreateIndexModel<ProductEntity>(compoundKeysDefinition, compoundIndexOptions);
        var existingIndexes = productsCollection.Indexes.List().ToList();
        bool indexAlreadyExists = existingIndexes.Any(index => index["name"] == compoundIndexOptions.Name);

        if (indexAlreadyExists)
        {
            Console.WriteLine($"Index {compoundIndexOptions.Name} already exists, delete it first");
            return;
        }
        
        await productsCollection.Indexes.CreateOneAsync(compoundIndexModel);
        Console.WriteLine($"Index {compoundIndexOptions.Name} has been created");
    }

    private static async Task RemoveCompoundProductIndexAsync(IMongoCollection<ProductEntity> productsCollection)
    {
        var indexName = "Rating_Price_Index";
        var existingIndexes = productsCollection.Indexes.List().ToList();
        bool indexExist = existingIndexes.Any(index => index["name"] == indexName);

        if (!indexExist)
        {
            Console.WriteLine($"Index {indexName} does not exist!");
            return;
        }
        
        await productsCollection.Indexes.DropOneAsync(indexName);
        Console.WriteLine($"Index '{indexName}' has been dropped.");
    }
}