using AutoMapper;
using DataAccess.Entities;
using Domain.Models;

namespace DataAccess.AutoMapper;

public class DataAccessProfile : Profile
{
    public DataAccessProfile()
    {
        CreateMap<ProductModel, ProductEntity>();
        CreateMap<ProductEntity, ProductModel>();
    }
}