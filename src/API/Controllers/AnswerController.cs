using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnswerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identity;
    private readonly IGetFilePath _filePath;

    public AnswerController(IMediator mediator, IIdentityService identity, IGetFilePath filePath)
    {
        _mediator = mediator;
        _identity = identity;
        _filePath = filePath;
    }

    [HttpPost("send-keyrate")]
    public async Task<ActionResult<SendAnswerResponse>> SendKeyRate([FromBody] SendKeyrateDto userUnswer)
    {
        // клиент присылает json только со значением ставки формата double, keycloakId из токена, остальное не нужно присылать
        // если пришлет разные объекты, то нужно создать Dto того, что присылает клиент и взять оттуда нужное

        // 1. Получить keycloakId из токена
        Guid keycloakId = _identity.GetKeycloakId();

        // 2. Добавить в command
        var command = new SendAnswerCommand { KeyRate = userUnswer.KeyRate, KeycloakId = keycloakId };

        // 3. Записать ставку в БД Answer и получить list<result>
        SendAnswerResponse result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("send-files")]
    public async Task<ActionResult> SendFile([FromForm] SendFileDto userUnswer)
    {
        // 1. Получить keycloakId из токена
        Guid keycloakId = _identity.GetKeycloakId();

        // 2. Проверяем, что файл есть
        if (userUnswer.File == null || userUnswer.File.Length == 0)
            return BadRequest("File is required");

        // 3. Проверка типа файла
        if (userUnswer.File.ContentType != "application/pdf")
        {
            return BadRequest("Можно загружать только PDF файлы");
        }
        else
        {
            // 3. Читаем файл в массив байт
            using var memoryStream = new MemoryStream();
            await userUnswer.File.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            // Получаем путь к папкам
            string filePath = _filePath.GetPath();

            // 4. Создаём команду
            var command = new SendFileCommand
            {
                KeycloakId = keycloakId,
                FileContent = fileBytes,
                Name = userUnswer.File.FileName,
                ContentType = userUnswer.File.ContentType,
                FileSize = userUnswer.File.Length,
                FilePath = filePath
            };

            // 5. Отправляем команду
            await _mediator.Send(command);

            return Ok("Файл успешно загружен");
        }
    }

    [HttpPost("send-keyrate_and_files")]
    public async Task<ActionResult<SendAnswerAndFileResponse>> SendKeyRateAndFiles([FromForm] SendKeyrateAndFilesDto userUnswer)
    {
        // 1. Получить keycloakId из токена
        Guid keycloakId = _identity.GetKeycloakId();

        // 2. Проверить файл
        if (userUnswer.File == null || userUnswer.File.Length == 0)
            return BadRequest("Требуется прикрепить файл");

        // 3. Проверка типа файла
        if (userUnswer.File.ContentType != "application/pdf")
        {
            return BadRequest("Можно загружать только PDF файлы");
        }
        else
        {
            // 3. Читаем файл в массив байт
            using var memoryStream = new MemoryStream();
            await userUnswer.File.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            // Получаем путь к папкам
            string filePath = _filePath.GetPath();

            // 4. Добавить в command
            var commandSendKeyrateAndFile = new SendAnswerAndFileCommand
            {
                KeycloakId = keycloakId,
                KeyRate = userUnswer.KeyRate,
                FileContent = fileBytes,
                Name = userUnswer.File.FileName,
                ContentType = userUnswer.File.ContentType,
                FileSize = userUnswer.File.Length,
                FilePath = filePath
            };

            // 5. Выполняем команду
            SendAnswerAndFileResponse results = await _mediator.Send(commandSendKeyrateAndFile);

            return Ok(results);
        }
    }

}
