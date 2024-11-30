using AxxesMarket.Shared.WebModels.Product;
using AxxesMarket.SPA.Client.Utils;
using BlazorState;
using MediatR;

namespace AxxesMarket.SPA.Client.Features.Products.Store;

public partial class ProductsState
{
    public class GetProductsHandler : ActionHandler<GetProductsAction>
    {
        private BlazorHttpClient _httpClient;

        public GetProductsHandler(IStore aStore, BlazorHttpClient blazorHttpClient) : base(aStore)
        {
            _httpClient = blazorHttpClient;
        }

        private ProductsState _state => Store.GetState<ProductsState>();

        public override async Task<Unit> Handle(GetProductsAction aAction, CancellationToken aCancellationToken)
        {
            var productsResponse = await _httpClient.GetAsync<IEnumerable<ProductResponse>>(ApiRoutes.GetProducts());
            if (!productsResponse.Success) return await Unit.Task;

            _state.AddProducts(productsResponse.Result);

            return await Unit.Task;
        }
    }
}
