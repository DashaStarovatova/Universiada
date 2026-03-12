namespace Domain;

public class Team
{

    public Guid Id { get; set; }
    public Guid KeycloakId { get; set; }
    public DateTime CreatedAt  { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }


    // Приватный конструктор (можно вызвать только внутри самого класса)
    // нужен для работы с библиотека Entity Framework для загрузки данных из БД
    // можно оставить пустым, тоже ок работает
    private Team()
    {
        Id = Guid.Empty;
        KeycloakId = Guid.Empty;
        Name = string.Empty;
        Login = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    // Публичный конструктор нужен для вызова снаружи
    public Team(Guid keycloakId, string name, string login) : this()
    {
        KeycloakId = keycloakId;
        Name = name;
        Login = login;
    }
}
