namespace Domain.Repositories;

public interface IAnswersRepository
{
    Task AddAsync(Answer answer, CancellationToken cancellationToken);
    Task<Answer?> GetLastAnswerAsync(Guid teamId, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
