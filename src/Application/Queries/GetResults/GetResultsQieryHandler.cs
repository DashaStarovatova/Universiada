using MediatR;
using Domain.Interfaces;
using Domain;

public class GetResultsQueryHandler : IRequestHandler<GetResultsQuery, List<Result>>
{
    private readonly IResultRepository _resultRepo;
    public GetResultsQueryHandler(IResultRepository resultRepo)
    {
        _resultRepo = resultRepo;
    }

    public async Task<List<Result>> Handle(GetResultsQuery query, CancellationToken cancellationToken)
    {
        var resultList = await _resultRepo.GetByTeamIdAsync(query.TeamId);
        return resultList;
    }
}

