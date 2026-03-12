using Domain;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


// Для установки EF:
// # Это ядро Entity Framework (базовый пакет)
// dotnet add package Microsoft.EntityFrameworkCore
// # Это инструменты для миграций (создания БД)
// dotnet add package Microsoft.EntityFrameworkCore.Tools
// # Провайдер памяти!
// dotnet add package Microsoft.EntityFrameworkCore.InMemory
// Ты сможешь позже заменить InMemory на любую настоящую БД (PostgreSQL, MySQL, SQL Server) без изменения кода репозиториев!
// Для PostgreSQL: dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL


public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;

    // Это DI, позволяет создать извне контекст и передать его, не создавая заново
    public TeamRepository(AppDbContext context)
    {
        _context = context; // присвоили контекст для дальнешего использования _context
    }

    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _context.Teams.FindAsync(id); // FindAsync ищет только по первичному ключу
    }

    public async Task<Team?> GetByKeycloakIdAsync(Guid keycloakId)
    {
        return await _context.Teams.FirstOrDefaultAsync(t => t.KeycloakId == keycloakId); // FirstOrDefaultAsync Ищет по любому условию
    }

    public async Task<Team?> GetByNameAsync(string name)
    {
        return await _context.Teams.FirstOrDefaultAsync(t => t.Name == name); // FirstOrDefaultAsync Ищет по любому условию
    }

    public async Task<List<Team>> GetAllAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }

    public void Update(Team team)
    {
        _context.Teams.Update(team);
    }

    public void Remove(Team team)
    {
        _context.Teams.Remove(team);
    }

    public async Task<bool> ExistsByKeycloakIdAsync(Guid keycloakId)
    {
        return await _context.Teams.AnyAsync(t => t.KeycloakId == keycloakId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
