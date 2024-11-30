using AxxesMarket.Api.Domain;
using AxxesMarket.Shared.WebModels.Product;

namespace AxxesMarket.Api.Mapping;

public static class ProductMappingExtensions
{
    public static ProductResponse MapToProductResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Price = product.Price,
            SoldOn = product.SoldOn
        };
    }

    public static Product MapToProduct(this CreateEditProductRequest request)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        return Product.Create(
            request.Id,
            request.Name,
            request.Description, 
            request.DetailedDescription, 
            request.PurchageDate!.Value, 
            request.HasWaranty, 
            request.Price!.Value, 
            null,
            "https://picsum.photos/1600/900");
    }

    public static ProductDetailResponse MapToDetailProductResponse(this Product product)
    {
        return new ProductDetailResponse
        {
            Description = product.Description,
            DetailedDescription = product.DetailedDescription,
            HasWaranty = product.HasWaranty,
            Id = product.Id,
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Price = product.Price,
            PurchageDate = product.PurchageDate,
            SoldOn = product.SoldOn
        };
    } 
}
