using Application.Models;
using Application.Services;

namespace Infrastructure.Services;

public class ResultsFacroty : IResultsFactory
{
    public ResultBase TeamAnswers()
    {
        return new SuccessResult();
    }

    public ResultBase TeamAnswersToday()
    {
        return new FailResult()
        {
            Description = "Команда уже отвечала сегодня"
        };
    }
}
