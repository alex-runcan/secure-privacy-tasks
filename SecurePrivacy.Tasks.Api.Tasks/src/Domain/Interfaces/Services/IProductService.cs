using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IProductService
{
    public Task<(long, List<ProductModel>)> GetProductsAsync(ProductSearchParamsModel searchParams);
    public Task<ProductModel> AddProductAsync(ProductModel product);
}