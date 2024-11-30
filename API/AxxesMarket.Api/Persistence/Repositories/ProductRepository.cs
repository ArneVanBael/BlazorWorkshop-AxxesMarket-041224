using AxxesMarket.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace AxxesMarket.Api.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AxxesMarketContext _context;

    public ProductRepository(AxxesMarketContext context)
    {
        _context = context;
    }

    public Guid CreateProduct(Product product)
    {
        _context.Products.Add(product);
        return product.Id;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetMyProductsAsync()
    {
        var name = "John Doe";
        if(Thread.CurrentPrincipal?.Identity?.IsAuthenticated == true)
        {
            name = Thread.CurrentPrincipal.Identity.Name;
        }

        return await _context.Products.Where(x => x.Owner.Equals(name)).ToListAsync();
    }
 
    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        return await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
    }

    public async Task UpdateProduct(Product product)
    {
        var originalProduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == product.Id);
        if (originalProduct is null) return;

        originalProduct.Update(product);
    }

    public async Task BuyProduct(Guid Id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == Id);
        if (product is null) return;

        product.Buy();
        await _context.SaveChangesAsync();
    }
}
