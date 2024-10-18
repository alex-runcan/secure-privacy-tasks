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
        await AddSeedProducts(productsCollection);
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

    private static async Task AddSeedProducts(IMongoCollection<ProductEntity> productsCollection)
    {
        var products = new List<ProductEntity>
        {
            new ProductEntity
            {
                Name = "Wireless Bluetooth Headphones",
                Description = "Noise-cancelling over-ear headphones with high-fidelity sound.",
                Price = 89.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Stainless Steel Water Bottle",
                Description = "Insulated bottle keeps drinks cold for 24 hours or hot for 12 hours.",
                Price = 25.50m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Smart LED Light Bulb",
                Description = "Energy-efficient bulb with adjustable color and brightness via app.",
                Price = 15.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            // Add the rest of the products here...
            new ProductEntity
            {
                Name = "Portable External Hard Drive 1TB",
                Description = "High-speed USB 3.0 external storage for PC and Mac.",
                Price = 59.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Wireless Charging Pad",
                Description = "Fast charging pad compatible with Qi-enabled devices.",
                Price = 19.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Fitness Activity Tracker",
                Description = "Track your daily activity, heart rate, and sleep patterns.",
                Price = 49.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Organic Green Tea Bags",
                Description = "Premium organic green tea for a refreshing and healthy drink.",
                Price = 9.00m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Memory Foam Pillow",
                Description = "Ergonomic pillow for improved sleep and neck support.",
                Price = 29.95m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "USB-C to HDMI Adapter",
                Description = "Connect your USB-C device to an HDMI display easily.",
                Price = 12.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Electric Kettle with Temperature Control",
                Description = "Boil water quickly with adjustable temperature settings.",
                Price = 39.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Compact Travel Umbrella",
                Description = "Windproof and waterproof umbrella, perfect for travel.",
                Price = 17.50m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Stainless Steel Chef Knife",
                Description = "Professional 8-inch chef knife for all your kitchen needs.",
                Price = 22.49m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Yoga Mat with Carrying Strap",
                Description = "Non-slip yoga mat ideal for yoga, pilates, and workouts.",
                Price = 18.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Bluetooth Shower Speaker",
                Description = "Waterproof speaker with suction cup for music in the shower.",
                Price = 21.99m,
                Rating = 3,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Laptop Cooling Pad",
                Description = "Keep your laptop cool with this quiet and efficient cooling pad.",
                Price = 24.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Adjustable Phone Stand",
                Description = "Desktop stand compatible with all smartphones and tablets.",
                Price = 10.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "LED Desk Lamp with USB Charging Port",
                Description = "Dimmable lamp with touch control and adjustable brightness.",
                Price = 34.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Noise Isolating In-Ear Earphones",
                Description = "Comfortable earbuds with clear sound and deep bass.",
                Price = 14.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Adjustable Dumbbell Set",
                Description = "Space-saving design for a full-body workout at home.",
                Price = 199.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Ceramic Coffee Mug with Lid",
                Description = "Stylish mug keeps your coffee hot and prevents spills.",
                Price = 16.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Wireless Ergonomic Mouse",
                Description = "Comfortable mouse with programmable buttons and adjustable DPI.",
                Price = 29.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "4K Ultra HD Action Camera",
                Description = "Capture your adventures with stunning 4K resolution.",
                Price = 89.95m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Smartphone Car Mount Holder",
                Description = "Securely attach your phone to your car's air vent.",
                Price = 13.49m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Rechargeable Electric Toothbrush",
                Description = "Promote oral health with multiple brushing modes.",
                Price = 39.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Virtual Reality Headset",
                Description = "Experience immersive VR content with this comfortable headset.",
                Price = 59.99m,
                Rating = 3,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Portable Bluetooth Speaker",
                Description = "Compact speaker with powerful sound and long battery life.",
                Price = 27.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Multi-Port USB Charger",
                Description = "Charge up to 5 devices simultaneously with fast charging.",
                Price = 22.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Wireless Video Doorbell",
                Description = "See who's at your door from your smartphone, with motion detection.",
                Price = 99.99m,
                Rating = 4,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Anti-Glare Screen Protector",
                Description = "Protect your laptop screen from scratches and reduce glare.",
                Price = 12.99m,
                Rating = 3,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Electric Standing Desk Converter",
                Description = "Convert your desk into a standing workstation effortlessly.",
                Price = 249.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            },
            new ProductEntity
            {
                Name = "Compact Mirrorless Camera",
                Description = "Capture high-quality photos with this lightweight camera.",
                Price = 499.99m,
                Rating = 5,
                CreatedAt = DateTime.UtcNow
            }
        };
        productsCollection.InsertMany(products);
    }
}