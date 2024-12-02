using AxxesMarket.Shared.Components.Store;
using AxxesMarket.Shared.Components.Utils;
using BlazorState;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AxxesMarket.Shared.Components;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection serviceCollection, bool addBlazorState = true)
    {
        serviceCollection.AddTransient<AntiforgeryHandler>();

        if(addBlazorState)
        {
            serviceCollection.AddBlazorState(opt =>
            {
                //opt.UseReduxDevToolsBehavior = true;
                opt.Assemblies = new Assembly[]
                {
                typeof(ApplicationState).Assembly,
                };
            });
        }
       
        serviceCollection.AddScoped<Translator>();
        serviceCollection.AddScoped<BlazorHttpClient>();
        serviceCollection.AddLocalization(opt => opt.ResourcesPath = "Resources");

        return serviceCollection;
    }
}
