using MediatR;
using Domain;
using Domain.Interfaces;
using Application.Interfaces;

public class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, bool>
{

    private readonly ILoadedFileRepository _loadedFileRepo;
    private readonly IUploadFile _uploadFile;
    private readonly IMediator _mediator;


    public SaveFileCommandHandler(ILoadedFileRepository loadedFileRepo,
    IUploadFile uploadFile, IMediator mediator)
    {
        _loadedFileRepo = loadedFileRepo;
        _uploadFile = uploadFile;
        _mediator = mediator;
    }

    public async Task<bool> Handle(SaveFileCommand command, CancellationToken cancellationToken)
    {

        // 1. Сохраняем метаданные (пока без пути)
        var file = new LoadedFile(
            command.TeamId,
            command.Name,
            "",  // ← временно пустой путь
            command.ContentType,
            command.FileSize
        );
        await _loadedFileRepo.AddAsync(file);
        await _loadedFileRepo.SaveAsync();

        // 3. Генерируем путь 
        var queryGetTeamLogin = await _mediator.Send(new GetTeamLoginQuery { TeamId = command.TeamId });
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

        return true;

    }
}

