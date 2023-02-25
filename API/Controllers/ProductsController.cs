using System.Net;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper
    )
    {
        _productRepo = productRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(Pagination<ProductToReturnDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
        [FromQuery] ProductSpecificationsParams productParams)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(productParams);

        var countSpecification = new ProductWithFiltersForCountSpecification(productParams);

        var totalItems = await _productRepo.CountAsync(countSpecification);

        var products = await _productRepo.ListAsync(specification);

        var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(
            productParams.PageIndex,
        productParams.PageSize,
        totalItems,
        data));
    }

    // Her API controller makes sure that this id is  
    // integer and if not it will return 400 Bad Request
    // if we removed the [ApiController] notaion we will get 204 no content
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productRepo.GetEntityWithSpecification(specification);

        if (product == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }


    [HttpGet("brands", Name = "GetProductBrands")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }


    [HttpGet("types", Name = "GetProductTypes")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductType>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }

}