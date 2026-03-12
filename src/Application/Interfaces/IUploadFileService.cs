namespace Application.Interfaces;
using Domain;

public interface IUploadFile
{
    Task Upload(string Path, byte[] content, CancellationToken cancellationToken);
}