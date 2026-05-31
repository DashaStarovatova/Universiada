using Domain;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AnswersRepository : IAnswersRepository
{
    private readonly AppDbContext _context;

    public AnswersRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Answer answer, CancellationToken cancellationToken)
    {
        return _context.AddAsync(answer, cancellationToken).AsTask();
    }

    public Task<Answer?> GetLastAnswerAsync(Guid teamId, CancellationToken cancellationToken)
    {
        return _context.Answers
            .Where(x => x.TeamId == teamId)
            .OrderBy(x => x.CreatedDate)
            .LastOrDefaultAsync(cancellationToken);
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
