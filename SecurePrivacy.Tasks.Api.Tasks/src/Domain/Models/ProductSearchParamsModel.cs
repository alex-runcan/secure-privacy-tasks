namespace Domain.Models;

public class ProductSearchParamsModel
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string? PriceSort { get; set; } = null;
    public int? RatingFilter { get; set; } = null;
}