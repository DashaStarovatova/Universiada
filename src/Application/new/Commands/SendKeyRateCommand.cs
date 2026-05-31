using Application.Models;
using MediatR;

namespace Application.Commands;

public record SendKeyRateCommand : IRequest<ResultBase>
{
    public required Guid TeamId { get; init; }
    public required string TeamName { get; init; }
    public required float KeyRate { get; init; }
}
