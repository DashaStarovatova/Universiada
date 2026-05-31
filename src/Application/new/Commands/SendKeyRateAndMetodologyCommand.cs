using Application.Models;
using MediatR;

namespace Application.Commands;

public record SendKeyRateAndMetodologyCommand
    : IRequest<ResultBase>
{
    public required Guid TeamId { get; init; }
    public required string TeamName { get; init; }
    public required float KeyRate { get; init; }
    public required string FileName { get; init; }
    public required byte[] FileData { get; init; }
}
