using AutoMapper;
using DataAccess.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<ProductEntity> _collection;
    
    public ProductRepository(IMongoDatabase database, IMapper mapper)
    {
        _mapper = mapper;
        _collection = database.GetCollection<ProductEntity>(ProductEntity.CollectionName);
    }

    public async Task<List<ProductModel>> GetProductsAsync(int page = 0, int limit = 50)
    {
        var filter = Builders<ProductEntity>.Filter.Empty;
        var productEntities = await _collection.Find(filter)
            .Skip(page * limit)
            .Limit(limit)
            .ToListAsync();
        return _mapper.Map<List<ProductEntity>, List<ProductModel>>(productEntities);
    }

    public async Task<ProductModel> AddProductAsync(ProductModel product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _collection.InsertOneAsync(productEntity);
        return _mapper.Map<ProductModel>(productEntity);
    }
}