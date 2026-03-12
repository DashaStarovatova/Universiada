namespace Domain.Interfaces;
using Domain;

public interface IResultRepository
{
    // Получить результат по ID
    Task<Result?> GetByIdAsync(Guid id);

    // Получить все результаты команды
    Task<List<Result>> GetByTeamIdAsync(Guid teamId);
    
    // Получить результаты за конкретный период (например, "231Q1")
    Task<Result?> GetByTeamIdAndPeriodAsync(Guid teamId, string period);
    
    // Получить результаты за период расчетов (реальные даты)
    Task<Result?> GetByTeamIdAndExactDateAsync(Guid teamId, DateTime to,  DateTime from);

    // Получить последний результат команды (самый свежий)
    Task<Result?> GetLatestByTeamIdAsync(Guid teamId);

    // Добавить новый результат
    Task AddAsync(Result results);

    // Обновить существующий результат
    void Update(Result results);

    // Удалить результат
    void Remove(Result results);

    // Сохранить в БД
    Task SaveAsync();

    // Проверить, есть ли результат за период
    Task<bool> ExistsForPeriodAsync(Guid teamId, string period);
}
