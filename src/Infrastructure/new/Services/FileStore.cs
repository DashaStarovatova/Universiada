using Application.Services;  // ← это должно быть
using System.IO;             // ← для File.WriteAllBytesAsync
using Domain;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class FileStore : IFileStore
{
    private readonly ILoadedFileRepository _file;


    public FileStore(
        ILoadedFileRepository file)
    {
        _file = file;
    }
    public async Task SaveAsync(
        Guid teamId,
        string fileName,
        byte[] fileData,
        CancellationToken cancellationToken)
    {
        var filePath = $"C:/Users/Darya/Desktop/testUniversiada/letsgo/{teamId}/{fileName}";

        await File.WriteAllBytesAsync(
            filePath,
            fileData,
            cancellationToken);
    }

    public async Task SaveToDatabaseAsync(Guid teamId, string fileName, byte[] fileData, CancellationToken cancellationToken)
    {
        var filePath = $"C:/Users/Darya/Desktop/testUniversiada/letsgo/{teamId}/{fileName}";
        long sizeInBytesLong = fileData.LongLength;

        var newFile = new LoadedFile(
            teamId: teamId,
            name: fileName,
            path: filePath,
            contentType: "pdf",
            fileSize: sizeInBytesLong
        );

        await _file.AddAndSaveAsync(newFile, cancellationToken);
    }

}
