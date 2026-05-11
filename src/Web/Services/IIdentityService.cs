using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

public interface IIdentityService
{
    public Task<Guid> GetUserIdAsync();
    public Task<string> GetUserNameAsync();
}

public class IdentityService : IIdentityService
{
    private readonly AuthenticationStateProvider _provider;

    public IdentityService(AuthenticationStateProvider provider)
    {
        _provider = provider;
    }

    public async Task<Guid> GetUserIdAsync()
    {
        var state = await _provider.GetAuthenticationStateAsync();

        return Guid.Parse(state.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    public async Task<string> GetUserNameAsync()
    {
        var state = await _provider.GetAuthenticationStateAsync();

        return state.User.FindFirstValue(ClaimTypes.GivenName)!;
    }
}
