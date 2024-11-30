using AxxesMarket.Api.Persistence.Repositories;

namespace AxxesMarket.Api.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private IProductRepository? _productRepository;
    private readonly AxxesMarketContext _context;

    public UnitOfWork(AxxesMarketContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
}
