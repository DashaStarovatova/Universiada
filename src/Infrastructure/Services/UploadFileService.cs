
using Domain;
using Application.Interfaces;
namespace Infrastructure.Services;
class UploadFile : IUploadFile
{
    public async Task Upload(string Path, byte[] content, CancellationToken cancellationToken) // если я не передам в метод cancellationToken, то будет по умолчанию 
    {
        await File.WriteAllBytesAsync(Path, content, cancellationToken); // Это метод .NET
    }



};

