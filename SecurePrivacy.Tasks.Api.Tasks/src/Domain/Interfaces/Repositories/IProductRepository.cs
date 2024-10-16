using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IProductRepository
{
    public Task<List<ProductModel>> GetProductsAsync(int offset = 0, int limit = 50);
    public Task<ProductModel> AddProductAsync(ProductModel product);
}