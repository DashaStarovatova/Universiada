using Domain;
using MediatR;


public class GetAnswerQuery : IRequest<Answer>
{
    public Guid TeamId { get; set; }
}