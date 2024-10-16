using API.DTOs.v1;
using AutoMapper;
using Domain.Models;

namespace API.AutoMapper;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<ProductModel, ProductResponseDto>();
        CreateMap<CreateProductRequestDto, ProductModel>();
    }
}