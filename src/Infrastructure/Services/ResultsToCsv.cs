using System.Text;
using Application.Interfaces;
using Application;  // подставь свой namespace для DTO
namespace Infrastructure.Services;

public class ResultsToCsv : IResultsToCsv
{
    public async Task<byte[]> GenerateCsvFromResultsAsync(List<ResultExportDto> results)
    {
        if (results == null || results.Count == 0)
            return Array.Empty<byte>();

        var csv = new StringBuilder();

        // Заголовки
        csv.AppendLine("TeamId,Period,NominalExchangeRate,CPI,RealExchangeRate,RealGDP,KeyRate");

        // Данные
        foreach (var item in results)
        {
            csv.AppendLine($"{item.Period},{item.NominalExchangeRate},{item.CPI},{item.RealExchangeRate},{item.RealGDP},{item.KeyRate}");
        }

        // csv.ToString() → строка
        // Encoding.UTF8.GetBytes(...) — строка → байты
        // Task.FromResult(...) — байты → Task
        // await — ждем Task
        return await Task.FromResult(Encoding.UTF8.GetBytes(csv.ToString()));
    }
}