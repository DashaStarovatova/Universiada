using MediatR;
using Domain;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

public class SendAnswerCommandHandler : IRequestHandler<SendAnswerCommand, SendAnswerResponse>
{

    private readonly IMediator _mediator;
    private readonly ILogger<SendAnswerCommandHandler> _logger;
    private readonly IAnswerRepository _answerRepo;

    public SendAnswerCommandHandler(IMediator mediator, ILogger<SendAnswerCommandHandler> logger, IAnswerRepository answerRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _answerRepo=answerRepo;
    }


    public async Task<SendAnswerResponse> Handle(SendAnswerCommand command, CancellationToken cancellation)
    {

        // 1. Получаем TeamId команды по KeycloakId
        Guid teamId = await _mediator.Send(new GetTeamIdByKeycloakIdQuery
        {
            KeycloakId = command.KeycloakId

        });

        Guid answerId = Guid.Empty;

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        cts.CancelAfter(TimeSpan.FromSeconds(30));
        try
        {

            // 1. Получаем TeamId команды по KeycloakId
            // teamId = await _mediator.Send(new GetTeamIdByKeycloakIdQuery
            // {
            //     KeycloakId = command.KeycloakId

            // }, cts.Token);
            DateTime today = DateTime.UtcNow;
            var isAnswerToday = _answerRepo.HasTeamSubmittedOnDateAsync(teamId, today);
            if (!await isAnswerToday){}else{return new SendAnswerResponse
            {
                Success = false,
                Message = "Команда уже отправила сегодня ответ."
            };};

            // 2. Вызываем комманду SaveKeyRateCommand
            // Записываем в БД Answer ответ команды
            answerId = await _mediator.Send(new SaveKeyRateCommand
            {
                KeyRate = command.KeyRate,
                TeamId = teamId
            }, cts.Token);


            
            await _mediator.Send(new RunMatlabCommand
            {
                KeyRate = command.KeyRate,
                IsRefresh = 0,
                TeamId = teamId,
                AnswerId = answerId
            }, cts.Token);  // ← передаём токен с таймаутом

            List<Result> teamResults = await _mediator.Send(new GetResultsQuery
            {
                TeamId = teamId
            }, cts.Token);

            return new SendAnswerResponse
            {
                Success = true,
                Message = "Расчёт выполнен",
                Results = teamResults,
                AnswerId = answerId

            };
        }
        catch (OperationCanceledException)
        {
            if (teamId == Guid.Empty)
            {
                // Логируем ошибку
                _logger.LogError("Ошибка при поиске пользователя в БД Team: {keycloakId}", command.KeycloakId);

                return new SendAnswerResponse
                {
                    Success = false,
                    Message = "Ошибка авторизации.",
                    ErrorCode = "TEAM_ERROR"
                };
            }
            else if (answerId == Guid.Empty)
            {

                _logger.LogError("Ошибка при записи в БД Answer: {keycloakId}", command.KeycloakId);

                return new SendAnswerResponse
                {
                    Success = false,
                    Message = "Ошибка загрузки ставки.",
                    ErrorCode = "ANSWER_ERROR"
                };


            }
            else
            {
                // Логируем таймаут
                _logger.LogWarning("Таймаут расчёта для AnswerId: {AnswerId}", answerId);

                return new SendAnswerResponse
                {
                    Success = false,
                    Message = "Расчёт займёт больше времени. Результаты появятся в ЛК позднее.",
                    AnswerId = answerId
                };

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"!!!!!!!!!! ОШИБКА: {ex.Message}");
            // Логируем ошибку
            _logger.LogError(ex, "Ошибка при расчёте для AnswerId: {AnswerId}", answerId);

            return new SendAnswerResponse
            {
                Success = false,
                Message = $"Произошла ошибка. Попробуйте позже. {teamId}",
                ErrorCode = "CALCULATION_ERROR"
            };
        }
    }
}



