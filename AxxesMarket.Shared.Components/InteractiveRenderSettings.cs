using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace AxxesMarket.Shared.Components;
public class InteractiveRenderSettings
{
    public static IComponentRenderMode? InteractiveServer { get; set; } =
       RenderMode.InteractiveServer;
    public static IComponentRenderMode? InteractiveAuto { get; set; } =
        RenderMode.InteractiveAuto;
    public static IComponentRenderMode? InteractiveWebAssembly { get; set; } =
        RenderMode.InteractiveWebAssembly;

    public static IComponentRenderMode InteractiveAutoNoPreRender { get; set; } =
        new InteractiveAutoRenderMode(prerender: false);

    public static IComponentRenderMode InteractiveServerNoPreRender { get; set; } 
        = new InteractiveAutoRenderMode(prerender: false);

    public static IComponentRenderMode InteractiveWebAssemblyNoPreRender { get; set; }
        = new InteractiveWebAssemblyRenderMode(prerender: false);

    public static void ConfigureBlazorHybridRenderModes()
    {
        InteractiveServer = null;
        InteractiveAuto = null;
        InteractiveWebAssembly = null;
        InteractiveWebAssemblyNoPreRender = null;
        InteractiveServerNoPreRender = null;
        InteractiveAutoNoPreRender = null;
    }
}
