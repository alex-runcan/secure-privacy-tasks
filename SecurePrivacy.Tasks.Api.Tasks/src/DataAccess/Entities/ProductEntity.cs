using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Entities;

public class ProductEntity
{
    public static string CollectionName = "products";
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
    [BsonElement("Description")]
    public string Description { get; set; } = "";
    [BsonElement("Price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    [BsonElement("Rating")]
    [Range(1, 5)]
    public int Rating { get; set; }
    [BsonElement("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}