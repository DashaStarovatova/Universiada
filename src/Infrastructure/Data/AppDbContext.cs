using Microsoft.EntityFrameworkCore;
using Domain; //Подключает слой Domain, чтобы видеть классы Team, Answer, LoadedFile, Result.
namespace Infrastructure.Data;


// Создает класс AppDbContext, который наследуется от DbContext. Т.е. имеет все возможности встроенного в EF DbContext
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)  
    // вызов конструктора родительского класса. : base(options) говорит: "Передай эти настройки родителю (DbContext)"
    {
        // Твой конструктор пустой { } - тебе ничего добавлять не нужно
        // без : base(options) пришлось бы вручную настраивать options
        // options: Какая БД (SQL Server, PostgreSQL, InMemory), строка подключения (где находится БД), доп параметры
    }

    // Объявляет, что у нас будет таблица для объектов Team, и обращаться к ней в коде мы будем через свойство Teams.
    // DbSet - это коллекция, через которую мы добавляем, ищем, удаляем команды.
    public DbSet<Team> Teams { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<LoadedFile> LoadedFiles { get; set; }
    public DbSet<Result> Results { get; set; }



    // protected — метод виден только внутри этого класса и наследников
    // override — мы переопределяем (заменяем) стандартную реализацию из DbContext
    // void — метод ничего не возвращает, просто настраивает
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Это вызов метода родительского класса (DbContext).
        base.OnModelCreating(modelBuilder);

        // ========== НАСТРОЙКА TEAM ==========
        modelBuilder.Entity<Team>(entity =>
        {
            // Первичный ключ
            entity.HasKey(t => t.Id);

            // Настройки полей
            entity.Property(t => t.Name)
                .IsRequired()           // NOT NULL
                .HasMaxLength(100);      // максимальная длина 100

            entity.Property(t => t.CreatedAt)
                .IsRequired();

            entity.Property(t => t.KeycloakId)
                .IsRequired();

            // Уникальный индекс для KeycloakId
            entity.HasIndex(t => t.KeycloakId)
                .IsUnique();

        });

        // ========== НАСТРОЙКА ANSWER ==========
        modelBuilder.Entity<Answer>(entity =>
        {
            // Первичный ключ
            entity.HasKey(a => a.Id);

            // Настройка связи с Team
            entity.HasOne<Team>()                    // у Answer есть одна Team
                .WithMany()                           // у Team много Answer
                .HasForeignKey(a => a.TeamId)         // внешний ключ - TeamId
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Cascade);    // при удалении Team удалять все Answer

            // Настройки полей
            entity.Property(a => a.AnswerValue)
                .IsRequired();

            entity.Property(a => a.CreatedAt)
                .IsRequired();

            entity.Property(a => a.CreatedDate)
                .IsRequired();
        });

        // ========== НАСТРОЙКА LOADEDFILE ==========
        modelBuilder.Entity<LoadedFile>(entity =>
        {
            // Первичный ключ
            entity.HasKey(l => l.Id);

            // Настройка связи с Team
            entity.HasOne<Team>()                    // у LoadedFile есть одна Team
                .WithMany()                           // у Team много LoadedFile
                .HasForeignKey(l => l.TeamId)         // внешний ключ - TeamId
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Cascade);    // при удалении Team удалять все файлы

            // Настройки полей
            entity.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(l => l.Path)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(l => l.ContentType)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(l => l.FileSize)
                .IsRequired();

            entity.Property(l => l.CreatedAt)
                .IsRequired();

            entity.Property(l => l.CreatedDate)
                .IsRequired();

        });

        // ========== НАСТРОЙКА RESULT ==========
        modelBuilder.Entity<Result>(entity =>
        {
            // Первичный ключ
            entity.HasKey(r => r.Id);

            // Настройка связи с Team
            entity.HasOne<Team>()                    // у Result есть одна Team
                .WithMany()                           // у Team много Result
                .HasForeignKey(r => r.TeamId)         // внешний ключ - TeamId
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Cascade);    // при удалении Team удалять все Result


            // Уникальность: у команды может быть только один результат за период
            entity.HasIndex(r => new { r.TeamId, r.Period })
                .IsUnique();

            // Настройки полей
            entity.Property(r => r.Period)
                .IsRequired()
                .HasMaxLength(10);  // "231Q1" - 5 символов

            entity.Property(r => r.NominalExchangeRate)
                .IsRequired();

            entity.Property(r => r.CPI)
                .IsRequired();

            entity.Property(r => r.RealExchangeRate)
                .IsRequired();

            entity.Property(r => r.RealGDP)
                .IsRequired();

            entity.Property(r => r.KeyRate)
                .IsRequired();

            entity.Property(r => r.CreatedAt)
                .IsRequired();


        });


    }
}