namespace Domain.Models;

public class ProductModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = "";
    public double Price { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}