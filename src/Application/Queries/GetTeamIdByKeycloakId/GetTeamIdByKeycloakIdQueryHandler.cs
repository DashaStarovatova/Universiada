using MediatR;
using Domain.Interfaces;
using Application;

public class GetTeamIdByKeycloakIdQueryHandler : IRequestHandler<GetTeamIdByKeycloakIdQuery, Guid>
{
    private readonly ITeamRepository _teamRepo;
    public GetTeamIdByKeycloakIdQueryHandler(ITeamRepository teamRepo)
    {
        _teamRepo = teamRepo;
    }

    public async Task<Guid> Handle(GetTeamIdByKeycloakIdQuery query, CancellationToken cancellationToken)
    {
        var team = await _teamRepo.GetByKeycloakIdAsync(query.KeycloakId);
        if (team != null)
        {
            return team.Id;
        }

        return Guid.Empty;
    }

}

