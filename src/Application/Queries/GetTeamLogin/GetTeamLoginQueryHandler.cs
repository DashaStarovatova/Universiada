using MediatR;
using Domain.Interfaces;

public class GetTeamLoginQueryHandler : IRequestHandler<GetTeamLoginQuery, string>
{
    private readonly ITeamRepository _teamRepo;
    public GetTeamLoginQueryHandler(ITeamRepository teamRepo)
    {
        _teamRepo = teamRepo;
    }

    public async Task<string> Handle(GetTeamLoginQuery query, CancellationToken cancellationToken)
    {
        var team = await _teamRepo.GetByIdAsync(query.TeamId);
        if (team != null)
        {
            var teamLogin = team.Login;
            return teamLogin;
        }

        return string.Empty;
    }

}

