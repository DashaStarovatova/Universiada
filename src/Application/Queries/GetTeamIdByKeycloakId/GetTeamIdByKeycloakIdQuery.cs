using MediatR;


public class GetTeamIdByKeycloakIdQuery : IRequest<Guid>
{
    public Guid KeycloakId { get; set; }
}