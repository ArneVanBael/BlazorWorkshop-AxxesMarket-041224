using AxxesMarket.Shared.WebModels.Product;
using AxxesMarket.Shared.WebModels.User;
using AxxesMarket.SPA.Client.Utils;
using BlazorState;
using MediatR;

namespace AxxesMarket.SPA.Client.Features.Profile.Store;

public partial class ProfileState
{
    public class GetUserSettingsHandler : ActionHandler<GetUserSettingsAction>
    {
        private BlazorHttpClient _httpClient;

        public GetUserSettingsHandler(IStore aStore, BlazorHttpClient blazorHttpClient) : base(aStore)
        {
            _httpClient = blazorHttpClient;
        }

        public override async Task<Unit> Handle(GetUserSettingsAction aAction, CancellationToken aCancellationToken)
        {
            var settings = new UserSettings
            {
                UserId = Guid.NewGuid(),
                Address = "test adres",
                Email = "email",
                FirstName = "Arne",
                LastName = "Van Bael"
            };

            var state = Store.GetState<ProfileState>();
            state.AddSettings(settings);

            return await Unit.Task;
        }
    }

    public class GetMyProductsHandler : ActionHandler<GetMyProductsAction>
    {
        private BlazorHttpClient _httpClient;


        public GetMyProductsHandler(IStore aStore, BlazorHttpClient httpClient) : base(aStore)
        {
            _httpClient = httpClient;
        }

        public override async Task<Unit> Handle(GetMyProductsAction aAction, CancellationToken aCancellationToken)
        {
            var productsResponse = await _httpClient.GetAsync<IEnumerable<ProductResponse>>(ApiRoutes.GetMyProducts());
            if (!productsResponse.Success) return await Unit.Task;

            var state = Store.GetState<ProfileState>();
            state.AddProducts(productsResponse.Result, clearProducts:true);

            return await Unit.Task;
        }
    }

    public class CreateProductHandler : ActionHandler<CreateProductAction>
    {
        private BlazorHttpClient _httpClient;
        private Translator _translator;

        public CreateProductHandler(IStore aStore, BlazorHttpClient httpClient, Translator translator) : base(aStore)
        {
            _httpClient = httpClient;
            _translator = translator;
        }

        public override async Task<Unit> Handle(CreateProductAction aAction, CancellationToken aCancellationToken)
        {
            // create call to api, update my products list or reload list
            var createResponse = await _httpClient.PostAsync<CreateEditProductRequest, Guid>(ApiRoutes.CreateProduct(), aAction.CreateEditProductRequest, successMessage: _translator["ProductCreatedSuccess"]);
            if (createResponse is null || !createResponse.Success) return await Unit.Task;

            var getProductsResponse = await _httpClient.GetAsync<IEnumerable<ProductResponse>>(ApiRoutes.GetMyProducts());
            if (!getProductsResponse.Success) return await Unit.Task;

            var state = Store.GetState<ProfileState>();
            state.AddProducts(getProductsResponse.Result, clearProducts: true);

            return await Unit.Task;
        }
    }

    public class UpdateProductHandler : ActionHandler<UpdateProductAction>
    {
        private BlazorHttpClient _httpClient;
        private Translator _translator;


        public UpdateProductHandler(IStore aStore, BlazorHttpClient httpClient, Translator translator) : base(aStore)
        {
            _httpClient = httpClient;
            _translator= translator;
        }

        public override async Task<Unit> Handle(UpdateProductAction aAction, CancellationToken aCancellationToken)
        {
            if (aAction.CreateEditProductRequest is null || !aAction.CreateEditProductRequest.Id.HasValue) return await Unit.Task;

            var updateResponse = await _httpClient.PutAsync<CreateEditProductRequest, Guid>(ApiRoutes.UpdateProduct(aAction.CreateEditProductRequest.Id.Value), aAction.CreateEditProductRequest, successMessage: _translator["ProductUpdatedSuccess"]);
            if (!updateResponse.Success) return await Unit.Task;

            var getProductsResponse = await _httpClient.GetAsync<IEnumerable<ProductResponse>>(ApiRoutes.GetMyProducts());
            if (!getProductsResponse.Success) return await Unit.Task;

            var state = Store.GetState<ProfileState>();
            state.AddProducts(getProductsResponse.Result, clearProducts: true);

            return await Unit.Task;
        }
    }
}
