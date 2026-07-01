using MediatR;
using Domain.Interfaces;
using Domain;

public class GetAnswerQueryHandler : IRequestHandler<GetAnswerQuery, Answer>
{
    private readonly IAnswerRepository _answerRepo;
    public GetAnswerQueryHandler(IAnswerRepository answerRepo)
    {
        _answerRepo = answerRepo;
    }

    public async Task<Answer> Handle(GetAnswerQuery query, CancellationToken cancellationToken)
    {
        var lastAnswer = await _answerRepo.GetLastByTeamIdAsync(query.TeamId);
        return lastAnswer;
    }
}

