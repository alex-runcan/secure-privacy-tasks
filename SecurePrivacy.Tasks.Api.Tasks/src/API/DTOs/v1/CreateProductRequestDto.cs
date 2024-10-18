namespace API.DTOs.v1;

public class CreateProductRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; } = "";
    public double Price { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}