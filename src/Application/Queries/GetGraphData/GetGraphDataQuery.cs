using MediatR;


public class GetGraphDataQuery : IRequest<List<GraphDataDto>>
{
    public Guid KeycloakId { get; set; }
}