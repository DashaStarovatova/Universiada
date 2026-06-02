using Domain;

namespace Application.Services;

public interface IAnswersResolver
{
    void AddToQueue(Answer answer);
    Task RunAsync(CancellationToken cancellationToken);
}
