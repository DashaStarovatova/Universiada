using MediatR;
using Domain;
using Domain.Interfaces;
using Application.Interfaces;

public class SendFileCommandHandler : IRequestHandler<SendFileCommand>
{

    private readonly ILoadedFileRepository _loadedFileRepo;
    private readonly IUploadFile _uploadFile;

    private readonly IMediator _mediator;

    public SendFileCommandHandler(ILoadedFileRepository loadedFileRepo,
    IUploadFile uploadFile, IMediator mediator)
    {
        _loadedFileRepo = loadedFileRepo;
        _uploadFile = uploadFile;
        _mediator = mediator;
    }
    public async Task Handle(SendFileCommand command, CancellationToken cancellationToken)
    {

        // 1. Получаем TeamId команды по KeycloakId
        Guid teamId = await _mediator.Send(new GetTeamIdByKeycloakIdQuery
        {
            KeycloakId = command.KeycloakId
        });

        // 2. Сохраняем метаданные (пока без пути)
        var file = new LoadedFile(
            teamId,
            command.Name,
            "",  // ← временно пустой путь
            command.ContentType,
            command.FileSize
        );
        await _loadedFileRepo.AddAsync(file);
        await _loadedFileRepo.SaveAsync();

        // 3. Генерируем путь 
        var queryGetTeamLogin = await _mediator.Send(new GetTeamLoginQuery { TeamId = teamId });
        string date = DateTime.Now.ToString("ddMM");
        var myPath = @$"{command.FilePath}{queryGetTeamLogin}";
        var filePath = Path.Combine(myPath, $"{date}_{queryGetTeamLogin}_{command.Name}");

        // 4. Загружаем на сервер
        await _uploadFile.Upload(filePath, command.FileContent, cancellationToken);

        // 5. Обновляем путь в метаданных
        // если с загрузкой на сервер произойдет проблема, то можно будет отследить записи о файлах без пути
        file.Path = filePath;
        _loadedFileRepo.Update(file);
        await _loadedFileRepo.SaveAsync();
    }
}

