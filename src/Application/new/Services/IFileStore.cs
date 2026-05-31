namespace Application.Services;

public interface IFileStore
{
    Task SaveAsync(Guid teamId, string fileName, byte[] fileData, CancellationToken cancellationToken);
}
