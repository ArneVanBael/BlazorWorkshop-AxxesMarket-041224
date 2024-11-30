using AxxesMarket.Api.Persistence.Repositories;

namespace AxxesMarket.Api.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    public IProductRepository ProductRepository { get; }
}
