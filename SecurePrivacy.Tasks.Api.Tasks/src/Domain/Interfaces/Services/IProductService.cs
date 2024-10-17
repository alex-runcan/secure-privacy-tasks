using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IProductService
{
    public Task<List<ProductModel>> GetProductsAsync(int page = 0, int limit = 50);
    public Task<ProductModel> AddProductAsync(ProductModel product);
}