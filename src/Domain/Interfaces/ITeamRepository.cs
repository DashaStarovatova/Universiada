namespace Domain.Interfaces;
using Domain;

public interface ITeamRepository

{
    // Получить команду по её внутреннему ID (Guid)
    // Вопросительный знак (?) означает: "может вернуть null".
    Task<Team?> GetByIdAsync(Guid id);

    // Получить команду по KeycloakId (тот, что из токена)
    Task<Team?> GetByKeycloakIdAsync(Guid keycloakId);

    // Получить команду по Name
    Task<Team?> GetByNameAsync(string name);

    // Получить все команды (например, для админки)
    Task<List<Team>> GetAllAsync();

    // Добавить новую команду (при регистрации)
    Task AddAsync(Team team);

    // Обновить существующую команду (если меняли название)
    void Update(Team team);

    // Удалить команду
    void Remove(Team team);

    // Проверить, существует ли команда с таким KeycloakId
    Task<bool> ExistsByKeycloakIdAsync(Guid keycloakId);

    Task SaveAsync();
    }