using Domain;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResultRepository : IResultRepository
{
    private readonly AppDbContext _context; // оюъявили переменную, сейчас это null, но может хранить ссылку на класс AppDbContext
    public ResultRepository(AppDbContext context)
    {
        _context = context; // присвоили ссылку на AppDbContext
    }


    public async Task<Result?> GetByIdAsync(Guid id)
    {
        return await _context.Results.FindAsync(id);
    }

    public async Task<List<Result>> GetByTeamIdAsync(Guid teamId)
    {
        return await _context.Results.Where(p => p.TeamId == teamId).ToListAsync();
    }

    public async Task<Result?> GetByTeamIdAndPeriodAsync(Guid teamId, string period)
    {
        return await _context.Results.FirstOrDefaultAsync(p => p.TeamId == teamId && p.Period == period);
    }

    public async Task<Result?> GetByTeamIdAndExactDateAsync(Guid teamId, DateTime from, DateTime to)
    {
        return await _context.Results.FirstOrDefaultAsync(p => p.TeamId == teamId
        && p.CreatedAt.Date >= from.Date && p.CreatedAt.Date <= to.Date);
    }

    public async Task<Result?> GetLatestByTeamIdAsync(Guid teamId)
    {
        return await _context.Results
       .Where(t => t.TeamId == teamId)        // фильтруем по команде
       .OrderByDescending(t => t.CreatedAt)   // сортируем от новых к старым (OrderBy (без Descending) = от старых к новым)
       .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Result results)
    {
        await _context.Results.AddAsync(results);
    }

    public void Update(Result results)
    {
        _context.Results.Update(results);
    }

    public void Remove(Result results)
    {
        _context.Results.Remove(results);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }


    public async Task<bool> ExistsForPeriodAsync(Guid teamId, string period)
    {
        return await _context.Results.AnyAsync(t => t.TeamId == teamId && t.Period == period);
    }
}