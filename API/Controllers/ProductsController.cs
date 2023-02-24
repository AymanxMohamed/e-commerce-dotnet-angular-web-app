using System.Net;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
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
    [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
    {
        var specification = new ProductsWithTypesAndBrandsSpecification();
        var products = await _productRepo.ListAsync(specification);
        return Ok(_mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    // Her API controller makes sure that this id is  
    // integer and if not it will return 400 Bad Request
    // if we removed the [ApiController] notaion we will get 204 no content
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(ProductToReturnDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productRepo.GetEntityWithSpecification(specification);
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