using AxxesMarket.Shared.WebModels.Product;
using BlazorState;

namespace AxxesMarket.SPA.Client.Features.Profile.Store;

public partial class ProfileState
{
    public class GetUserSettingsAction : IAction { }
    public class GetMyProductsAction : IAction { }
    public class CreateProductAction : IAction
    {
        public CreateEditProductRequest CreateEditProductRequest { get; private set; }

        public CreateProductAction(CreateEditProductRequest createEditProductRequest)
        {
            CreateEditProductRequest = createEditProductRequest;
        }
    }
    public class UpdateProductAction : IAction
    {
        public CreateEditProductRequest CreateEditProductRequest { get; private set; }

        public UpdateProductAction(CreateEditProductRequest createEditProductRequest)
        {
            CreateEditProductRequest = createEditProductRequest;
        }
    }
}
