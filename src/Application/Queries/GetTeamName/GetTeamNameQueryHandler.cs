using MediatR;
using Domain.Interfaces;

public class GetTeamNameQueryHandler : IRequestHandler<GetTeamNameQuery, string>
{
    private readonly ITeamRepository _teamRepo;
    public GetTeamNameQueryHandler(ITeamRepository teamRepo)
    {
        _teamRepo = teamRepo;
    }

    public async Task<string> Handle(GetTeamNameQuery query, CancellationToken cancellationToken)
    {

        var team = await _teamRepo.GetByKeycloakIdAsync(query.KeycloakId);
        if (team != null)
        {
            var teamName = team.Name;
            // var teamName = "testTeam";
            return teamName;
        }

        return string.Empty;
    }

}

