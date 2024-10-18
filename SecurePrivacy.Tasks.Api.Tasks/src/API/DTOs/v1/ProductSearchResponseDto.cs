namespace API.DTOs.v1;

public class ProductSearchResponseDto
{
    public long Count { get; set; }
    public List<ProductResponseDto> Products { get; set; }
}