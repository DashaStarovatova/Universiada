using Application.Commands;
using Application.Models;
using Application.Services;
using Domain;
using Domain.Repositories;
using MediatR;

namespace Application.CommandHandlers;

public class SendKeyRateAndMetodologyCommandHandler
    : IRequestHandler<SendKeyRateAndMetodologyCommand, ResultBase>
{
    private readonly IAnswersRepository _repository;
    private readonly IAnswersResolver _backgroundTasks;
    private readonly IFileStore _store;
    private readonly IResultsFactory _results;

    public SendKeyRateAndMetodologyCommandHandler(
        IAnswersRepository repository,
        IResultsFactory results,
        IAnswersResolver backgroundTasks,
        IFileStore store)
    {
        _repository = repository;
        _results = results;
        _backgroundTasks = backgroundTasks;
        _store = store;
    }

    public async Task<ResultBase> Handle(
        SendKeyRateAndMetodologyCommand command,
        CancellationToken cancellationToken)
    {
        var lastAnswer = await _repository.GetLastAnswerAsync(
            command.TeamId,
            cancellationToken);

        if (lastAnswer?.IsTodayAnswer() is true)
        {
            return _results.TeamAnswersToday();
        }

        var answer = new Answer(
            command.TeamId,
            command.KeyRate);

        await _repository.AddAsync(answer, cancellationToken);
        await _repository.SaveAsync(cancellationToken);

        _backgroundTasks.AddToQueue(answer);

        await _store.SaveAsync(
            command.TeamId,
            command.FileName,
            command.FileData,
            cancellationToken);

        return _results.TeamAnswers();
    }
}
