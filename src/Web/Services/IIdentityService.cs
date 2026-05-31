using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

public interface IIdentityService
{
    public Task<Guid> GetUserIdAsync();
    public Task<string> GetUserNameAsync();
    Task<string?> GetIdTokenAsync();
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

    public async Task<string?> GetIdTokenAsync()
    {
        var state = await _provider.GetAuthenticationStateAsync();

        // Ищем claim с именем "id_token"
        var idToken = state.User.FindFirst("id_token")?.Value;

        // Или альтернативный вариант поиска:
        // var idToken = state.User.Claims.FirstOrDefault(c => c.Type == "id_token")?.Value;

        return idToken;
    }
}
