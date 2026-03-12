namespace Application.Interfaces;
public interface IResultsToCsv
{
    Task<byte[]> GenerateCsvFromResultsAsync(List<ResultExportDto> results);
}