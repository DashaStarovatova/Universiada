using Domain;
using MediatR;


public class GetResultsQuery : IRequest<List<Result>>
{
    public Guid TeamId { get; set; }
}