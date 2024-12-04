using AxxesMarket.SPA.Client;
using AxxesMarket.SPA.Server;
using Duende.AccessTokenManagement.OpenIdConnect;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// ADD BFF AUTHENTICATION HERE
builder.Services.AddBff(o => o.RevokeRefreshTokenOnLogout = false)
    .AddServerSideSessions()
    .AddRemoteApis();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSharedServices();
builder.Services.AddSingleton<IUserTokenStore, CustomTokenStore>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddUserAccessTokenHttpClient("backend", configureClient: client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? string.Empty);
});
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";
})
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";
        options.Cookie.SameSite = SameSiteMode.None;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.CorrelationCookie.SameSite = SameSiteMode.None;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
        options.CorrelationCookie.HttpOnly = true;
        options.NonceCookie.SameSite = SameSiteMode.None;
        options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
        options.NonceCookie.HttpOnly = true;
        options.SkipUnrecognizedRequests = true;

        options.Authority = "https://demo.duendesoftware.com/";

        // confidential client using code flow + PKCE
        //options.ClientId = "AxxesMarket.SPA";
        options.ClientId = "interactive.confidential";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;

        // request scopes + refresh tokens
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("api");
        options.Scope.Add("offline_access");

        options.Events.OnTokenValidated = async n =>
        {
            var tokenStore = n.HttpContext.RequestServices.GetRequiredService<IUserTokenStore>();
            var exp = DateTimeOffset.UtcNow.AddSeconds(Double.Parse(n.TokenEndpointResponse.ExpiresIn));
            var userToken = new UserToken
            {
                AccessToken = n.TokenEndpointResponse.AccessToken,
                Expiration = exp,
                RefreshToken = n.TokenEndpointResponse.RefreshToken
            };

            await tokenStore.StoreTokenAsync(n.Principal, userToken);
        };
    });

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
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.UseRequestLocalization(o =>
{
    o.SupportedCultures = new CultureInfo[] { new CultureInfo("nl-BE"), new CultureInfo("en-US") };
    o.SupportedUICultures = new CultureInfo[] { new CultureInfo("nl-BE"), new CultureInfo("en-US") };
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();


app.UseEndpoints(endpoints =>
{
    endpoints.MapBffManagementEndpoints();
    var backendApiUrl = $"{builder.Configuration["ApiUrl"] ?? string.Empty}/api";
    // remote endpoint
    endpoints.MapRemoteBffApiEndpoint("/api", backendApiUrl)
        .WithOptionalUserAccessToken();

    // MVC controllers
    endpoints.MapControllers()
        .RequireAuthorization()    // no anonymous access
        .AsBffApiEndpoint();       // BFF pre/post processing
});


app.Run();
