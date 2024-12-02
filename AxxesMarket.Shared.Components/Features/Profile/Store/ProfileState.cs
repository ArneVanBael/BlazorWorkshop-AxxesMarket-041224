using AxxesMarket.Shared.WebModels.Product;
using AxxesMarket.Shared.WebModels.User;
using BlazorState;
using System.Collections.ObjectModel;

namespace AxxesMarket.SPA.Client.Features.Profile.Store;

public partial class ProfileState : State<ProfileState>
{
    private List<ProductResponse> _myProducts = new();
    public IReadOnlyCollection<ProductResponse> MyProducts => new ReadOnlyCollection<ProductResponse>(_myProducts);
    public UserSettings UserSettings { get; private set; }

    public override void Initialize()
    {
    }

    public void AddSettings(UserSettings settings) => UserSettings = settings;

    public void AddProducts(IEnumerable<ProductResponse> products, bool clearProducts = false)
    {
        if(clearProducts)
        {
            _myProducts = new List<ProductResponse>();
        }

        _myProducts.AddRange(products.OrderBy(x => x.Name));
    }
    public void UpdateProduct(ProductResponse product)
    {
        // get product
        var productToUpdate = _myProducts.SingleOrDefault(x => x.Id == product.Id);

        if (productToUpdate is null) return;

        _myProducts.Remove(productToUpdate);
        _myProducts.Add(product);
        _myProducts = _myProducts.OrderBy(x => x.Name).ToList();
    }
}
