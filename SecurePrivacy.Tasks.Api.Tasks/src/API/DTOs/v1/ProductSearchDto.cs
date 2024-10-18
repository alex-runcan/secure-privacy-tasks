using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace API.DTOs.v1;

public record ProductSearchDto
{
    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 10;
    [AllowedValues(["ascend", "descend", null])]
    public string? PriceSort { get; init; } = null;
    [AllowedValues([1, 2, 3, 4, 5, null])] 
    public int? RatingFilter { get; init; } = null;
}