using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

// IValueResolver<sourceEntity, DestinationEntity, DestinationValueType
public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration _configuration;

    public ProductUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(
        Product source,
        ProductToReturnDto destination,
        string destMember,
        ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return $"{_configuration["ApiUrl"]}{source.PictureUrl}";
        }
        return null;
    }
}