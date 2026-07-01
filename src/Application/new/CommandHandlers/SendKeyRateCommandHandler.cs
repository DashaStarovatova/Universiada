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

        var nullCount = 0;
        if (command.IsNul is true)
        {
            nullCount = (lastAnswer?.NullCount ?? 0) + 1;
        }
        else
        {
            nullCount = lastAnswer?.NullCount ?? 0;
        }

        var answer = new Answer(
            command.TeamId,
            command.KeyRate,
            nullCount
            );

        await _repository.AddAsync(answer, cancellationToken);
        await _repository.SaveAsync(cancellationToken);

        _backgroundTasks.AddToQueue(answer);

        return _results.TeamAnswers();
    }
}
