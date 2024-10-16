using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<ProductModel>> GetProductsAsync(int offset = 0, int limit = 50)
    {
        return _productRepository.GetProductsAsync(offset, limit);
    }

    public Task<ProductModel> AddProductAsync(ProductModel product)
    {
        return _productRepository.AddProductAsync(product);
    }
}