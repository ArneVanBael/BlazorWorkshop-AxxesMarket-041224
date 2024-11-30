using Duende.AccessTokenManagement.OpenIdConnect;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace AxxesMarket.SPA.Server;

public class CustomTokenStore : IUserTokenStore
{
    ConcurrentDictionary<string, UserToken> _tokens = new ConcurrentDictionary<string, UserToken>();

    public Task ClearTokenAsync(ClaimsPrincipal user, UserTokenRequestParameters parameters = null)
    {
        var sub = user.FindFirst("sub").Value;
        _tokens.TryRemove(sub, out _);
        return Task.CompletedTask;
    }

    public Task<UserToken> GetTokenAsync(ClaimsPrincipal user, UserTokenRequestParameters parameters = null)
    {
        var sub = user.FindFirst("sub").Value;
        _tokens.TryGetValue(sub, out var value);
        return Task.FromResult(value);
    }

    public Task StoreTokenAsync(ClaimsPrincipal user, UserToken token, UserTokenRequestParameters parameters = null)
    {
        var sub = user.FindFirst("sub").Value;
        _tokens[sub] = token;
        return Task.CompletedTask;
    }
}
