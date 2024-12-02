using AxxesMarket.Shared.WebModels.Product;
using BlazorState;
using System.Collections.ObjectModel;

namespace AxxesMarket.Shared.Components.Features.Products.Store;

public partial class ProductsState : State<ProductsState>
{
    private List<ProductResponse> _products = new();
    public IReadOnlyCollection<ProductResponse> Products => new ReadOnlyCollection<ProductResponse>(_products);

    public void AddProducts(IEnumerable<ProductResponse> products) => _products.AddRange(products);

    public override void Initialize()
    {
    }
}
