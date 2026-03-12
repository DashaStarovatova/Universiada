using MediatR;
using Domain.Interfaces;

public class GetGraphDataQueryHandler : IRequestHandler<GetGraphDataQuery, List<GraphDataDto>>
{
    private readonly IResultRepository _resultRepo;
    private readonly IMediator _mediator;

    public GetGraphDataQueryHandler(ITeamRepository teamRepo, IResultRepository resultRepo, IMediator mediator)
    {
        _resultRepo = resultRepo;
        _mediator = mediator;
    }

    public async Task<List<GraphDataDto>> Handle(GetGraphDataQuery query, CancellationToken cancellationToken)
    {

        // 1. Получаем TeamId команды по KeycloakId
        var teamId = await _mediator.Send(new GetTeamIdByKeycloakIdQuery
        {
            KeycloakId = query.KeycloakId
        });

        var resultList = await _resultRepo.GetByTeamIdAsync(teamId);

        // Укоротим историю для отображения на графике
        var shortList = resultList.Skip(0).ToList();

        return shortList.Select(r => new GraphDataDto
        {
            Period = r.Period,
            CPI = r.CPI,
            KeyRate = r.KeyRate,
            CPITarget = 4.0
        }).ToList();

    }

}

