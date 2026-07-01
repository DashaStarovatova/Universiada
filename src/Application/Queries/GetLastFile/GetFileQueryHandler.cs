using MediatR;
using Domain.Interfaces;
using Domain;

public class GetLastFileQueryHandler : IRequestHandler<GetLastFileQuery, LoadedFile>
{
    private readonly ILoadedFileRepository _fileLoadedRepo;
    public GetLastFileQueryHandler(ILoadedFileRepository fileLoadedRepo)
    {
        _fileLoadedRepo = fileLoadedRepo;
    }

    public async Task<LoadedFile> Handle(GetLastFileQuery query, CancellationToken cancellationToken)
    {
        var lastFile = await _fileLoadedRepo.GetLatestByTeamIdAsync(query.TeamId, cancellationToken);
        return lastFile;
    }
}

