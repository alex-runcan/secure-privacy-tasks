using API.DTOs.v1;
using Asp.Versioning;
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService,
        IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ProductSearchResponseDto>> GetProducts([FromQuery] ProductSearchDto productSearchParams)
    {
        var searchParamsModel = _mapper.Map<ProductSearchParamsModel>(productSearchParams);
        var (count, productModels) = await _productService.GetProductsAsync(searchParamsModel);
        var productDtos = _mapper.Map<List<ProductModel>, List<ProductResponseDto>>(productModels); 
        return Ok(new ProductSearchResponseDto
        {
            Count = count,
            Products = productDtos,
        });
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> AddProduct([FromBody] CreateProductRequestDto productDto)
    {
        var productModel = await _productService.AddProductAsync(_mapper.Map<ProductModel>(productDto));
        return Ok(_mapper.Map<ProductResponseDto>(productModel));
    }
}