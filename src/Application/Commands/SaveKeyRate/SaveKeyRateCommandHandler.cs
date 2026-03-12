using MediatR;
using Domain;
using Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

public class SaveKeyRateHandler : IRequestHandler<SaveKeyRateCommand, Guid>
{
    private readonly ITeamRepository _teamRepo;
    private readonly IAnswerRepository _answerRepo;

    public SaveKeyRateHandler(ITeamRepository teamRepo, IAnswerRepository answerRepo)
    {
        _teamRepo = teamRepo;
        _answerRepo = answerRepo;
    }

    public async Task<Guid> Handle(SaveKeyRateCommand command, CancellationToken cancellationToken)
    {
        var newAnswer = new Answer(command.TeamId, command.KeyRate);
        await _answerRepo.AddAsync(newAnswer);
        await _answerRepo.SaveAsync();

        Answer? lastAnswer = await _answerRepo.GetLastByTeamIdAsync(command.TeamId);
        if (lastAnswer != null)
        {
            Guid answerId = lastAnswer.Id;
            return answerId;
        }
        else
        {
            return Guid.Empty;
        }
    }
}