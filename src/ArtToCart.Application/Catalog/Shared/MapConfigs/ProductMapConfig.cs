using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;

using Mapster;

namespace ArtToCart.Application.Catalog.Shared.MapConfigs;

public class ProductMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CatalogItem, ProductDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Size, src => src.Size)
            .Map(dest => dest.SellerId, src => src.SellerId)
            .Map(dest => dest.CatalogType, src => src.CatalogType.Type)
            .Map(dest => dest.CatalogTypeId, src => src.CatalogTypeId.Value)
            .Map(dest => dest.Images, src => src.Images.Adapt<IEnumerable<ProductImageDto>>())
            .PreserveReference(true);

        config.NewConfig<ProductImage, ProductImageDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.IsMain, src => src.IsMain)
            .Map(dest => dest.CatalogItemId, src => src.CatalogItemId.Value)
            .PreserveReference(true);
    }
}