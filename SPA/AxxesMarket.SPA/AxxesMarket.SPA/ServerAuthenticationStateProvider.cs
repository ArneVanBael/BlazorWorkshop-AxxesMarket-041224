using AxxesMarket.SPA.Client.BFF;
using Duende.AccessTokenManagement.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AxxesMarket.SPA.Server;

public class ServerAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserTokenStore _userTokenStore;

    public ServerAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor, IUserTokenStore userTokenStore)
    {
        _httpContextAccessor = httpContextAccessor;
        _userTokenStore = userTokenStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if(_httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated == true)
        {
            var identity = new ClaimsIdentity(
                              nameof(ClientAuthenticationStateProvider),
                              "name",
                              "role");

            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value.ToString() ?? "no value"));
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        } else
        {
            return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());
        }
    }
}
