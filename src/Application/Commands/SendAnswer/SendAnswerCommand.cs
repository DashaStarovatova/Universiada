using MediatR;


public class SendAnswerCommand : IRequest<SendAnswerResponse>
{
    public Guid KeycloakId { get; set; }
    public double KeyRate { get; set; }
}