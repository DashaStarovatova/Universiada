using System.Security.Claims;

public interface IIdentityService
{
    Guid GetKeycloakId();
}

public class RealIdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RealIdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetKeycloakId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("User is not authenticated");

        // Используем правильное имя claim
        var keycloakIdString = user.FindFirstValue(ClaimTypes.NameIdentifier); ;

        if (string.IsNullOrEmpty(keycloakIdString))
            throw new InvalidOperationException("User ID not found in token");

        if (!Guid.TryParse(keycloakIdString, out Guid keycloakId))
            throw new InvalidOperationException($"Invalid user ID format: {keycloakIdString}");

        return keycloakId;
    }
}