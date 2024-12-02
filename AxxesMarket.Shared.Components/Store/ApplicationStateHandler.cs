using AxxesMarket.SPA.Client.Shared.Toasts;
using BlazorState;
using MediatR;
using Microsoft.JSInterop;
using System.Resources;

namespace AxxesMarket.Shared.Components.Store;
public partial class ApplicationState
{
    public class LoadInitialApplicationStateHandler : ActionHandler<LoadInitialApplicationStateAction>
    {
        private ApplicationState _applicationState => Store.GetState<ApplicationState>();

        public LoadInitialApplicationStateHandler(IStore aStore) : base(aStore)
        {
        }

        public override async Task<Unit> Handle(LoadInitialApplicationStateAction aAction, CancellationToken aCancellationToken)
        {
            _applicationState.LoggedInUserName = aAction.UserName?.Trim();
            _applicationState.LoggedInUserEmail = aAction.UserEmail?.Trim();
            _applicationState.ApplicationName = aAction.ApplicationName?.Trim();
            return await Unit.Task;
        }
    }

    public class ToggleSidebarMenuHandler : ActionHandler<ToggleSidebarMenuAction>
    {
        private ApplicationState _applicationState => Store.GetState<ApplicationState>();

        public ToggleSidebarMenuHandler(IStore aStore) : base(aStore)
        {
        }

        public override async Task<Unit> Handle(ToggleSidebarMenuAction aAction, CancellationToken aCancellationToken)
        {
            _applicationState.IsMenuVisible = !_applicationState.IsMenuVisible;
            return await Unit.Task;
        }
    }

    public class OpenCloseModalHandler : ActionHandler<OpenCloseModalAction>
    {
        private IJSRuntime _jsRuntime;

        public OpenCloseModalHandler(IStore aStore, IJSRuntime jsRuntime) : base(aStore)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<Unit> Handle(OpenCloseModalAction aAction, CancellationToken aCancellationToken)
        {
            var state = Store.GetState<ApplicationState>();
            if(aAction.IsOpen)
            {
                state.OpenModal();
                await _jsRuntime.InvokeVoidAsync("modalOpen");
            } else
            {
                state.CloseModal();
                await _jsRuntime.InvokeVoidAsync("modalClose");
            }

            return await Unit.Task;
        }
    }

    public class AddSuccessToastHandler : ActionHandler<AddSuccessToastAction>
    {
        public AddSuccessToastHandler(IStore aStore) : base(aStore)
        {
        }

        public override async Task<Unit> Handle(AddSuccessToastAction aAction, CancellationToken aCancellationToken)
        {
            var toast = new ToastMessageItem { Text = aAction.Text, Type = ToastMessageType.Success };
            var state = Store.GetState<ApplicationState>();

            state.AddToastMessage(toast);

            return await Unit.Task;
        }
    }

    public class AddErrorToastHandler : ActionHandler<AddErrorToastAction>
    {
        public AddErrorToastHandler(IStore aStore) : base(aStore)
        {
        }

        public override async Task<Unit> Handle(AddErrorToastAction aAction, CancellationToken aCancellationToken)
        {
            var toast = new ToastMessageItem { Text = aAction.Text, Type = ToastMessageType.Error };
            var state = Store.GetState<ApplicationState>();

            state.AddToastMessage(toast);

            return await Unit.Task;
        }
    }

    public class RemoveToastHandler : ActionHandler<RemoveToastAction>
    {
        public RemoveToastHandler(IStore aStore) : base(aStore)
        {
        }

        public override async Task<Unit> Handle(RemoveToastAction aAction, CancellationToken aCancellationToken)
        {
            var state = Store.GetState<ApplicationState>();
            state.RemoveToastMessage(aAction.Id);

            return await Unit.Task;
        }
    }
}
    