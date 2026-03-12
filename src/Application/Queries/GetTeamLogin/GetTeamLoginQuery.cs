using MediatR;


public class GetTeamLoginQuery : IRequest<string>
{
    public Guid TeamId { get; set; }
}