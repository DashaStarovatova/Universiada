using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identity;

    public TeamController(IMediator mediator, IIdentityService identity)
    {
        _mediator = mediator;
        _identity = identity;
    }

    [HttpGet("get-team-name")]
    public async Task<ActionResult<string>> SendKeyRate()
    {

        // 1. Получить keycloakId из токена
        Guid keycloakId = _identity.GetKeycloakId();

        // 2. Создаем запрос на получение данных
        var query = new GetTeamNameQuery { KeycloakId = keycloakId };

        // 3. Получаем Period, CPI, Keyrate, CPI target
        var teamName = await _mediator.Send(query);

        return Ok(teamName);
    }


}
