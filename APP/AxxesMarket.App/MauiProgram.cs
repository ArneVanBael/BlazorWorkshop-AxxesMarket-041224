using AxxesMarket.Shared.Components;
using Microsoft.Extensions.Logging;

namespace AxxesMarket.App;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddHttpClient("backend", configureClient: client =>
        {
            client.BaseAddress = new Uri("https://localhost:7138");
        });
        builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));
        InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();
        builder.Services.AddSharedServices(addBlazorState: true);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
