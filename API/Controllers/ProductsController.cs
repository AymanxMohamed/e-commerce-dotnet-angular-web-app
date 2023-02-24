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
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProduct(int id) =>
        Ok(await _repository.GetProductByIdAsync(id));
}