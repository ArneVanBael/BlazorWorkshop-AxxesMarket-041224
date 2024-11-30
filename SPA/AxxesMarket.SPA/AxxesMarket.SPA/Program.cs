using AxxesMarket.SPA.Client;
using AxxesMarket.SPA.Server;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// ADD BFF AUTHENTICATION HERE
//builder.Services.AddBff()
//    .AddServerSideSessions()
//    .AddRemoteApis();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSharedServices();

//builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

//builder.Services.AddUserAccessTokenHttpClient("backend", configureClient: client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? string.Empty);
//});
//builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// use authentication and authorizatin and bff here
//app.UseAuthentication();
//app.UseBff();
//app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();


//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapBffManagementEndpoints();
//    var backendApiUrl = $"{builder.Configuration["ApiUrl"] ?? string.Empty}/api";
//    // remote endpoint
//    endpoints.MapRemoteBffApiEndpoint("/api", backendApiUrl)
//        .WithOptionalUserAccessToken();

//    // MVC controllers
//    endpoints.MapControllers()
//        .RequireAuthorization()    // no anonymous access
//        .AsBffApiEndpoint();       // BFF pre/post processing
//});

app.Run();
