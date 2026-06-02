using Application.Commands;
using Application.Models;
using Application.Services;
using Domain;
using Domain.Repositories;
using MediatR;

namespace Application.CommandHandlers;

public class SendKeyRateCommandHandler
    : IRequestHandler<SendKeyRateCommand, ResultBase>
{
    private readonly IAnswersRepository _repository;
    private readonly IResultsFactory _results;
    private readonly IAnswersResolver _backgroundTasks;

    public SendKeyRateCommandHandler(
        IAnswersRepository repository,
        IResultsFactory results,
        IAnswersResolver backgroundTasks)
    {
        _repository = repository;
        _results = results;
        _backgroundTasks = backgroundTasks;
    }

    public async Task<ResultBase> Handle(
        SendKeyRateCommand command,
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

        return _results.TeamAnswers();
    }
}
