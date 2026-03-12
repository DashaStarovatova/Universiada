using MediatR;
using Domain;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

public class SendAnswerAndFileCommandHandler : IRequestHandler<SendAnswerAndFileCommand, SendAnswerAndFileResponse>
{

    private readonly IMediator _mediator;
    private readonly ILogger<SendAnswerCommandHandler> _logger;
    private readonly IAnswerRepository _answerRepo;

    public SendAnswerAndFileCommandHandler(IMediator mediator, ILogger<SendAnswerCommandHandler> logger, IAnswerRepository answerRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _answerRepo = answerRepo;
    }


    public async Task<SendAnswerAndFileResponse> Handle(SendAnswerAndFileCommand command, CancellationToken cancellation)
    {
        Guid teamId = Guid.Empty;
        Guid answerId = Guid.Empty;
        bool isFileLoad = false;

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        cts.CancelAfter(TimeSpan.FromSeconds(30));
        try
        {
            // 1. Получаем TeamId команды по KeycloakId
            teamId = await _mediator.Send(new GetTeamIdByKeycloakIdQuery
            {
                KeycloakId = command.KeycloakId
            }, cts.Token);


            // 3. Сохраняем файл на сервер, записываем метаданные в БД LoadedFile 
            isFileLoad = await _mediator.Send(new SaveFileCommand
            {
                TeamId = teamId,
                FileContent = command.FileContent,
                Name = command.Name,
                ContentType = command.ContentType,
                FileSize = command.FileSize,
                FilePath = command.FilePath

            }, cts.Token);


            DateTime today = DateTime.UtcNow;
            var isAnswerToday = _answerRepo.HasTeamSubmittedOnDateAsync(teamId, today);
            if (!await isAnswerToday) { }
            else
            {
                return new SendAnswerAndFileResponse
                {
                    Success = false,
                    Message = "Команда уже отправила сегодня ответ.",
                    IsFileLoaded = isFileLoad
                };
            }
            ;
            // 3. Вызываем комманду SaveKeyRateCommand
            // Записываем в БД Answer ответ команды
            answerId = await _mediator.Send(new SaveKeyRateCommand
            {
                KeyRate = command.KeyRate,
                TeamId = teamId
            }, cts.Token);

            // 4. Запускаем расчет в Матлаб
            await _mediator.Send(new RunMatlabCommand
            {
                KeyRate = command.KeyRate,
                IsRefresh = 0,
                TeamId = teamId,
                AnswerId = answerId
            }, cts.Token);  // ← передаём токен с таймаутом

            // 5. Получаем все результаты команды
            List<Result> teamResults = await _mediator.Send(new GetResultsQuery
            {
                TeamId = teamId
            }, cts.Token);

            // 6. Возвращаем ответ с результатами
            return new SendAnswerAndFileResponse
            {
                Success = true,
                Message = "Расчёт выполнен. Файл загружен.",
                Results = teamResults,
                AnswerId = answerId,
                IsFileLoaded = isFileLoad
            };
        }
        catch (OperationCanceledException)
        {
            if (teamId == Guid.Empty)
            {
                // Логируем ошибку
                _logger.LogError("Ошибка при поиске пользователя в БД Team: {keycloakId}", command.KeycloakId);

                return new SendAnswerAndFileResponse
                {
                    Success = false,
                    Message = "Ошибка авторизации.",
                    ErrorCode = "TEAM_ERROR",
                    IsFileLoaded = isFileLoad
                };
            }
            else if (isFileLoad == false)
            {
                _logger.LogError("Ошибка при сохранении файла: {keycloakId}", command.KeycloakId);

                return new SendAnswerAndFileResponse
                {
                    Success = false,
                    Message = "Ошибка загрузки файла.",
                    ErrorCode = "FILE_ERROR",
                    IsFileLoaded = isFileLoad
                };
            }
            else if (answerId == Guid.Empty)
            {

                _logger.LogError("Ошибка при записи в БД Answer: {keycloakId}", command.KeycloakId);

                return new SendAnswerAndFileResponse
                {
                    Success = false,
                    Message = "Ошибка загрузки ставки.",
                    ErrorCode = "ANSWER_ERROR",
                    IsFileLoaded = isFileLoad
                };

            }
            else
            {
                // Логируем таймаут
                _logger.LogWarning("Таймаут расчёта для AnswerId: {AnswerId}", answerId);

                return new SendAnswerAndFileResponse
                {
                    Success = false,
                    Message = "Расчёт займёт больше времени. Результаты появятся в ЛК позднее.",
                    ErrorCode = "TIMEOUT",
                    AnswerId = answerId,
                    IsFileLoaded = isFileLoad
                };

            }
        }
        catch (Exception ex)
        {
            // Логируем ошибку
            _logger.LogError(ex, "Ошибка при расчёте для AnswerId: {AnswerId}", answerId);

            return new SendAnswerAndFileResponse
            {
                Success = false,
                Message = "Произошла ошибка. Попробуйте позже.",
                ErrorCode = "CALCULATION_ERROR",
                IsFileLoaded = isFileLoad
            };
        }
    }
}



