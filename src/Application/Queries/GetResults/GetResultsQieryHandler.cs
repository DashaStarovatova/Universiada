using MediatR;
using Domain.Interfaces;
using Domain;

public class GetResultsQueryHandler : IRequestHandler<GetResultsQuery, List<Result>>
{
    private readonly ITeamRepository _teamRepo;
    private readonly IResultRepository _resultRepo;
    public GetResultsQueryHandler(ITeamRepository teamRepo, IResultRepository resultRepo)
    {
        _teamRepo = teamRepo;
        _resultRepo = resultRepo;
    }

    public async Task<List<Result>> Handle(GetResultsQuery query, CancellationToken cancellationToken)
    {
        var resultList = await _resultRepo.GetByTeamIdAsync(query.TeamId);
        return resultList;

    }

}

