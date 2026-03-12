using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GraphController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identity;


    public GraphController(IMediator mediator, IIdentityService identity)
    {
        _mediator = mediator;
        _identity = identity;
    }

    [HttpGet("get-data")]
    public async Task<ActionResult<List<GraphDataDto>>> SendKeyRate()
    {

        // 1. Получить keycloakId из токена
        Guid keycloakId = _identity.GetKeycloakId();


        // 2. Создаем запрос на получение данных
        var query = new GetGraphDataQuery { KeycloakId = keycloakId };

        // 3. Получаем Period, CPI, Keyrate, CPI target
        var result = await _mediator.Send(query);

        return Ok(result);
    }


}
