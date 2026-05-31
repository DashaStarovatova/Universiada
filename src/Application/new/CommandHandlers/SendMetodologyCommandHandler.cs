using Application.Commands;
using Application.Models;
using Application.Services;
using MediatR;

namespace Application.CommandHandlers;

public class SendMetodologyCommandHandler
    : IRequestHandler<SendMetodologyCommand, ResultBase>
{
    private readonly IFileStore _store;
    private readonly IResultsFactory _results;

    public SendMetodologyCommandHandler(
        IFileStore store,
        IResultsFactory results)
    {
        _store = store;
        _results = results;
    }

    public async Task<ResultBase> Handle(
        SendMetodologyCommand command,
        CancellationToken cancellationToken)
    {
        await _store.SaveAsync(
            command.TeamId,
            command.FileName,
            command.FileData,
            cancellationToken);

        return _results.TeamAnswers();
    }
}
