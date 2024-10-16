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
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService,
        IMapper mapper)
    {
        _logger = logger;
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetProducts([FromQuery] int offset = 0, [FromQuery] int limit = 50)
    {
        var productModels = await _productService.GetProductsAsync(offset, limit);
        return Ok(_mapper.Map<List<ProductModel>, List<ProductResponseDto>>(productModels));
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> AddProduct([FromBody] CreateProductRequestDto productDto)
    {
        var productModel = await _productService.AddProductAsync(_mapper.Map<ProductModel>(productDto));
        return Ok(_mapper.Map<ProductResponseDto>(productModel));
    }
}