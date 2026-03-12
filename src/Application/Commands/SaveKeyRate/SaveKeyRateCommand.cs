using MediatR;


public class SaveKeyRateCommand : IRequest<Guid>
{
    public Guid TeamId { get; set; }
    public double KeyRate { get; set; }
}