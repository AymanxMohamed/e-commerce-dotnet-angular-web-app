using System.Net;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    public ProductsController(IProductRepository repository) => _repository = repository;

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductss() =>
        Ok(await _repository.GetProductsAsync());


    // Her API controller makes sure that this id is  
    // integer and if not it will return 400 Bad Request
    // if we removed the [ApiController] notaion we will get 204 no content
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProduct(int id) =>
        Ok(await _repository.GetProductByIdAsync(id));

    [HttpGet("brands", Name = "GetProductBrands")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
        Ok(await _repository.GetPruductBrandsAsync());

    [HttpGet("types", Name = "GetProductTypes")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductType>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() =>
        Ok(await _repository.GetProductTypesAsync());
}