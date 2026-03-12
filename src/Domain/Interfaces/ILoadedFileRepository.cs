namespace Domain.Interfaces;
using Domain;

public interface ILoadedFileRepository
{
    // Получить файл по ID
    Task<LoadedFile?> GetByIdAsync(Guid id);
    
    // Получить все файлы команды
    Task<List<LoadedFile>> GetByTeamIdAsync(Guid teamId);
    
    // Получить файлы команды определённого типа (методология, ответ и т.д.)
    Task<List<LoadedFile>> GetByTeamIdAndTypeAsync(Guid teamId, string contentType);
    
    // Получить файлы, загруженные за период
    Task<List<LoadedFile>> GetByDateRangeAsync(DateTime from, DateTime to);
    
    // Получить последний файл команды определённого типа
    Task<LoadedFile?> GetLatestByTeamIdAndTypeAsync(Guid teamId, string contentType);
    
    // Добавить новый файл
    Task AddAsync(LoadedFile file);
    
    // Обновить информацию о файле
    void Update(LoadedFile file);
    
    // Удалить файл (из БД)
    void Remove(LoadedFile file);

    // Сохранить в БД
    Task SaveAsync();
    
    // Получить все файлы (для админки)
    Task<List<LoadedFile>> GetAllAsync();
}
