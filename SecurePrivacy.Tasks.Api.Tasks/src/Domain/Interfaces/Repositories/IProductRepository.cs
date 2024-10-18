using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IProductRepository
{
    public Task<(long, List<ProductModel>)> GetProductsAsync(ProductSearchParamsModel searchParams);
    public Task<ProductModel> AddProductAsync(ProductModel product);
}