using MediatR;


public class GetTeamNameQuery : IRequest<string>
{
    public Guid KeycloakId { get; set; }
}