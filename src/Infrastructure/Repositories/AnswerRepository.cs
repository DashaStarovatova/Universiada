using Domain;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly AppDbContext _context;
    public AnswerRepository(AppDbContext context)
    {
        _context = context; // присвоили контекст для дальнешего использования _context
    }

    public async Task<Answer?> GetByIdAsync(Guid id)
    {
        return await _context.Answers.FindAsync(id);
    }

    public async Task<List<Answer>> GetByTeamIdAsync(Guid teamId)
    {
        return await _context.Answers
           .Where(t => t.TeamId == teamId)
           .ToListAsync();
    }

    public async Task<List<Answer>> GetByTeamIdAndPeriodAsync(Guid teamId, DateTime from, DateTime to)
    {
        return await _context.Answers
          .Where(t => t.TeamId == teamId
                    && t.CreatedAt >= from
                    && t.CreatedAt <= to)
          .ToListAsync();
    }

    public async Task AddAsync(Answer answer)
    {
        await _context.Answers.AddAsync(answer);
    }

    public void Update(Answer answer)
    {
        _context.Answers.Update(answer);

    }


    public void Remove(Answer answer)
    {
        _context.Answers.Remove(answer);
    }

    // Сохранить изменения в БД
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasTeamSubmittedOnDateAsync(Guid teamId, DateTime date)

    {
        return await _context.Answers
            .AnyAsync(t => t.TeamId == teamId
                        && t.CreatedAt.Date == date.Date); // сравниваем только ДАТУ
    }


public async Task<Answer?> GetLastByTeamIdAsync(Guid teamId)
{
    return await _context.Answers
        .Where(t => t.TeamId == teamId)
        .OrderByDescending(a => a.CreatedAt)  // сортировка по дате
        .FirstOrDefaultAsync();
}

}