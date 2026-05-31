using System.Collections.Concurrent;
using Application.Services;
using Domain;

namespace Infrastructure.Services;

public class MatlabResolver : IAnswersResolver
{
    private readonly ConcurrentQueue<Answer> _answers;

    public MatlabResolver()
    {
        _answers = new ConcurrentQueue<Answer>();
    }

    public void AddToQueue(Answer answer)
    {
        _answers.Enqueue(answer);
    }
}
