using Domain;
using MediatR;


public class GetLastFileQuery : IRequest<LoadedFile>
{
    public Guid TeamId { get; set; }
}