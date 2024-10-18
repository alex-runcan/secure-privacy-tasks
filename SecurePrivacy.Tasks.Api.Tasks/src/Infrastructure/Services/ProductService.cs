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

    public async Task<(long, List<ProductModel>)> GetProductsAsync(ProductSearchParamsModel searchParams)
    {
        return await _productRepository.GetProductsAsync(searchParams);
    }

    public async Task<ProductModel> AddProductAsync(ProductModel product)
    {
        return await _productRepository.AddProductAsync(product);
    }
}