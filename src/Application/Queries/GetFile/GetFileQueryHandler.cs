using MediatR;
using Domain;
using Domain.Interfaces;

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, List<ResultExportDto>>  // ← исправили
{
    private readonly ITeamRepository _teamRepo;
    private readonly IResultRepository _resultRepo;

    public GetFileQueryHandler(ITeamRepository teamRepo, IResultRepository resultRepo)
    {
        _teamRepo = teamRepo;
        _resultRepo = resultRepo;
    }

    public async Task<List<ResultExportDto>> Handle(GetFileQuery query, CancellationToken cancellationToken)
    {
        var team = await _teamRepo.GetByKeycloakIdAsync(query.KeycloakId);
        
        if (team == null)
        {
            return new List<ResultExportDto>();  // ← возвращаем пустой список
        }

        List<Result> results = await _resultRepo.GetByTeamIdAsync(team.Id);

        // Преобразуем список Result в список ResultExportDto
        var exportDtos = results.Select(r => new ResultExportDto
        {
            Period = r.Period,
            NominalExchangeRate = r.NominalExchangeRate,
            CPI = r.CPI,
            RealExchangeRate = r.RealExchangeRate,
            RealGDP = r.RealGDP,
            KeyRate = r.KeyRate
        }).ToList();

        return exportDtos;
    }
}
