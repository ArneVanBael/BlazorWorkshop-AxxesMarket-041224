using AxxesMarket.SPA.Client.Store;
using BlazorState;
using BlazorState.Features.JavaScriptInterop;
using BlazorState.Features.Routing;
using BlazorState.Pipeline.ReduxDevTools;
using Microsoft.AspNetCore.Components;

namespace AxxesMarket.SPA.Client;

public partial class Routes : BlazorStateComponent
{
    [Inject] private JsonRequestHandler? JsonRequestHandler { get; set; }
    //[Inject] private ReduxDevToolsInterop? ReduxDevToolsInterop { get; set; }
    [Inject] private RouteManager RouteManager { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //await ReduxDevToolsInterop!.InitAsync();
        //await JsonRequestHandler!.InitAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await Mediator.Send(new ApplicationState.LoadInitialApplicationStateAction("Axxes Market"));
        await Task.CompletedTask;
    }
}
