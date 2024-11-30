using AxxesMarket.SPA.Client.BFF;
using AxxesMarket.SPA.Client.Store;
using AxxesMarket.SPA.Client.Utils;
using BlazorState;
using System.Reflection;

namespace AxxesMarket.SPA.Client;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<AntiforgeryHandler>();
        serviceCollection.AddCascadingAuthenticationState();
        serviceCollection.AddBlazorState(opt =>
        {
            //opt.UseReduxDevToolsBehavior = true;
            opt.Assemblies = new Assembly[]
            {
                typeof(ApplicationState).Assembly,
            };
        });

        //serviceCollection.AddScoped<Translator>();
        serviceCollection.AddScoped<BlazorHttpClient>();
        serviceCollection.AddLocalization(opt => opt.ResourcesPath = "Resources");

        return serviceCollection;
    }
}
