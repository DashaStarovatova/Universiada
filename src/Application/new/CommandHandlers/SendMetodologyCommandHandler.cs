using Application.Commands;
using Application.Models;
using Application.Services;
using Domain.Interfaces;
using Domain;
using MediatR;

namespace Application.CommandHandlers;

public class SendMetodologyCommandHandler
    : IRequestHandler<SendMetodologyCommand, ResultBase>
{
    private readonly IFileStore _store;
    private readonly ILoadedFileRepository _fileLoadedRepository;
    private readonly IResultsFactory _results;
    private readonly ILoadedFileRepository _file;


    public SendMetodologyCommandHandler(
        IFileStore store,
        ILoadedFileRepository fileLoadedRepository,
        IResultsFactory results,
        ILoadedFileRepository file)
    {
        _store = store;
        _fileLoadedRepository = fileLoadedRepository;
        _results = results;
        _file = file;
    }

    public async Task<ResultBase> Handle(
        SendMetodologyCommand command,
        CancellationToken cancellationToken)
    {
        var lastFile = await _fileLoadedRepository.GetLatestByTeamIdAsync(
            command.TeamId,
            cancellationToken
        );

        if (lastFile?.IsTodayFile() is true)
        {
            return _results.TeamAnswersToday();
        }

        await _store.SaveAsync(
            command.TeamId,
            command.FileName,
            command.FileData,
            cancellationToken);

        await _store.SaveToDatabaseAsync(
            command.TeamId,
            command.FileName,
            command.FileData,
            cancellationToken);

        return _results.TeamAnswers();
    }
}
