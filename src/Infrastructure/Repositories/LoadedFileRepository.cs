using Domain;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LoadedFileRepository : ILoadedFileRepository
{
    private readonly AppDbContext _context;

    public LoadedFileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoadedFile?> GetByIdAsync(Guid id)
    {
        return await _context.LoadedFiles.FindAsync(id);
    }

    public async Task<List<LoadedFile>> GetByTeamIdAsync(Guid teamId)
    {
        return await _context.LoadedFiles
        .Where(t => t.TeamId == teamId)
        .ToListAsync();
    }

    public async Task<List<LoadedFile>> GetByTeamIdAndTypeAsync(Guid teamId, string contentType)
    {
        return await _context.LoadedFiles
        .Where(t => t.TeamId == teamId && t.ContentType == contentType)
        .ToListAsync();
    }

    public async Task<List<LoadedFile>> GetByDateRangeAsync(DateTime from, DateTime to)
    {
        return await _context.LoadedFiles
        .Where(t => t.CreatedDate >= from && t.CreatedDate <= to)
        .ToListAsync();
    }

    public async Task<LoadedFile?> GetLatestByTeamIdAndTypeAsync(Guid teamId, string contentType)
    {
        return await _context.LoadedFiles
        .Where(t => t.TeamId == teamId && t.ContentType == contentType)
        .OrderByDescending(t => t.CreatedDate)
        .FirstOrDefaultAsync();
    }

    public async Task AddAsync(LoadedFile file)
    {
        await _context.LoadedFiles.AddAsync(file);
        await _context.SaveChangesAsync();
    }

    public void Update(LoadedFile file)
    {
        _context.LoadedFiles.Update(file);
    }

    public void Remove(LoadedFile file)
    {
        _context.LoadedFiles.Remove(file);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<LoadedFile>> GetAllAsync()
    {
        return await _context.LoadedFiles.ToListAsync();
    }
}
