namespace Domain.Interfaces;
using Domain;

public interface IAnswerRepository
{
    // Получить ответ по его ID
    Task<Answer?> GetByIdAsync(Guid id);

    // Получить все ответы команды
    Task<List<Answer>> GetByTeamIdAsync(Guid teamId);

    // Получить ответы команды за определённый период (если нужно)
    Task<List<Answer>> GetByTeamIdAndPeriodAsync(Guid teamId, DateTime from, DateTime to);

    // Добавить новый ответ
    Task AddAsync(Answer answer);

    // Обновить существующий ответ (если можно менять)
    void Update(Answer answer);

    // Удалить ответ
    void Remove(Answer answer);

    // Сохранить изменения
    Task SaveAsync();

    // Проверить, отправляла ли команда ответ на заданную дату
    Task<bool> HasTeamSubmittedOnDateAsync(Guid teamId, DateTime date);

    // Получить последний Answer по TeamId 
    Task<Answer?> GetLastByTeamIdAsync(Guid teamId);
}
