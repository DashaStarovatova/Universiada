using Application.Services;

namespace Infrastructure.Services;

public class FileStore : IFileStore
{
    public async Task SaveAsync(
        Guid teamId,
        string fileName,
        byte[] fileData,
        CancellationToken cancellationToken)
    {
        var filePath = $"{teamId}/{fileName}";

        await File.WriteAllBytesAsync(
            filePath,
            fileData,
            cancellationToken);
    }
}
