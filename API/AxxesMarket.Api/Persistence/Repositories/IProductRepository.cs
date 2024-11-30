using AxxesMarket.Api.Domain;

namespace AxxesMarket.Api.Persistence.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid productId);
    Guid CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task BuyProduct(Guid Id);
    Task<IEnumerable<Product>> GetMyProductsAsync();
}
