using Application.Models;

namespace Application.Services;

public interface IResultsFactory
{
    ResultBase TeamAnswers();
    ResultBase TeamAnswersToday();
}
