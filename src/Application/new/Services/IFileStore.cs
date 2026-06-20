namespace Application.Services;
using Domain;

public interface IFileStore
{
    Task SaveAsync(Guid teamId, string fileName, byte[] fileData, CancellationToken cancellationToken);
    Task SaveToDatabaseAsync(Guid teamId, string fileName, byte[] fileData, CancellationToken cancellationToken);
}
