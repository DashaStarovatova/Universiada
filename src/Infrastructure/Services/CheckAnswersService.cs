// dotnet add package Hangfire.AspNetCore
// dotnet add package Hangfire.PostgreSql

using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Commands;


namespace Infrastructure.Services;

public class CheckAnswersService
{
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    // Hangfire будет сам создавать экземпляр этого класса через DI,
    // поэтому ему нужны зависимости в конструкторе
    public CheckAnswersService(AppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    // Это метод, который будет выполняться по расписанию
     public async Task RunCheck(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var targetDates = new List<DateTime>
        {
            new DateTime(2026, 6, 9),
            new DateTime(2026, 2, 20),
            new DateTime(2026, 3, 25),
        };

        var isTargetDay = !targetDates.Any(d => d == today);
        if (isTargetDay is false)
        {
            return; // сегодня не день проверки
        }

        // логика проверки
        var teamIds = await _context.Answers
            .Select(a => a.TeamId)
            .Distinct()
            .ToListAsync(cancellationToken);

        foreach (var teamId in teamIds)
        {
            var lastAnswer = await _context.Answers
                .Where(a => a.TeamId == teamId)
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastAnswer == null)
            {
                 var command = new SendKeyRateCommand
                {
                    TeamId = teamId,
                    KeyRate = 10.0f, // значение ставки из нашей истории
                    IsNul = true
                };

                await _mediator.Send(command, cancellationToken);               
            } 
            else if (lastAnswer.CreatedDate.Date != DateTime.UtcNow.Date)
            {
                var command = new SendKeyRateCommand
                {
                    TeamId = teamId,
                    KeyRate = lastAnswer.AnswerValue,
                    IsNul = true
                };

                await _mediator.Send(command, cancellationToken);
            }


            Console.Write(teamId);
            Console.Write("done hangfire!");
        }
    }
}
