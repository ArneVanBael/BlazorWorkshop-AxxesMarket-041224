using AxxesMarket.SPA.Client;
using AxxesMarket.SPA.Client.BFF;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSharedServices();
// authentication state and authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();

builder.Services.AddHttpClient("BFF", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<AntiforgeryHandler>();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BFF"));

var host = builder.Build();

// set the default culture and supported culture list
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("getCookie", ".AspNetCore.Culture");
CultureInfo culture;
if (!string.IsNullOrWhiteSpace(result))
{
    var items = result.Split('|');
    culture = new CultureInfo(items[0].Split("=")[1]);
}
else
{
    await jsInterop.InvokeVoidAsync("setCookie", ".AspNetCore.Culture", "en-US", 100);
    culture = new CultureInfo("en-US");
}
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
