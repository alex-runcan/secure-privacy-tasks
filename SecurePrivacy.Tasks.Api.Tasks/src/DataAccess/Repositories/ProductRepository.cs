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

    public async Task<(long, List<ProductModel>)> GetProductsAsync(ProductSearchParamsModel searchParams)
    {
        var filter = Builders<ProductEntity>.Filter.Empty;
        if (searchParams.RatingFilter is not null)
        {
            filter = Builders<ProductEntity>.Filter.Eq(product => product.Rating, searchParams.RatingFilter);
        }
        var count = await _collection.CountDocumentsAsync(filter);
        var findQuery = _collection.Find(filter);
        if (searchParams.PriceSort is not null)
        {
            findQuery = findQuery.Sort((searchParams.PriceSort == "ascend")
                ? Builders<ProductEntity>.Sort.Ascending(product => product.Price)
                : Builders<ProductEntity>.Sort.Descending(product => product.Price));
        }
        
        var productEntities = await findQuery.Skip((searchParams.PageIndex - 1) * searchParams.PageSize)
            .Limit(searchParams.PageSize)
            .ToListAsync();
        var productModels = _mapper.Map<List<ProductEntity>, List<ProductModel>>(productEntities);
        return (count, productModels);
    }

    public async Task<ProductModel> AddProductAsync(ProductModel product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        await _collection.InsertOneAsync(productEntity);
        return _mapper.Map<ProductModel>(productEntity);
    }
}