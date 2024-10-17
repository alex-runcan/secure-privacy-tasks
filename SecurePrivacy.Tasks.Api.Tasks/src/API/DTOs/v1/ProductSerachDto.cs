using System.ComponentModel.DataAnnotations;

namespace API.DTOs.v1;

public class ProductSerachDto
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    [AllowedValues(["ascend", "descend", null])]
    public string PriceSort { get; set; }
    [AllowedValues([1, 2, 3, 4, 5])]
    public int RatingFilter { get; set; }
}