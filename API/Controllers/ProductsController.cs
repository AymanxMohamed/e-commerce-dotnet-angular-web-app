using System.Net;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;

    public ProductsController(
        IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo
    )
    {
        _productRepo = productRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _productRepo.ListAllAsync());
    }

    // Her API controller makes sure that this id is  
    // integer and if not it will return 400 Bad Request
    // if we removed the [ApiController] notaion we will get 204 no content
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return Ok(await _productRepo.GetByIdAsync(id));
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