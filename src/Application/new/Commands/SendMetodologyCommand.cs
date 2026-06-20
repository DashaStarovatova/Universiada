using Application.Models;
using MediatR;

namespace Application.Commands;

public record SendMetodologyCommand : IRequest<ResultBase>
{
    public required Guid TeamId { get; init; }
    // public required string TeamName { get; init; }
    public required string FileName { get; init; }
    public required byte[] FileData { get; init; }
}
