

using AutoMapper;
using InventoryApi.Domain.Entities;
using InventoryApi.Shared.DTOs;

namespace InventoryApi.Shared.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductDto, Product>();
    }
}
