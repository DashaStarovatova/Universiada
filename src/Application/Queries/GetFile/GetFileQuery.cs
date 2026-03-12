using MediatR;


public class GetFileQuery : IRequest<List<ResultExportDto>>
{
    public Guid KeycloakId { get; set; }
}