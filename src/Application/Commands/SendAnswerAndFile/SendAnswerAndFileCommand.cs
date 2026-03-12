using MediatR;


public class SendAnswerAndFileCommand : IRequest<SendAnswerAndFileResponse>
{
    public Guid KeycloakId { get; set; }
    public double KeyRate { get; set; }
    public byte[] FileContent { get; set; } = new byte[0];
    public string Name { get; set; } = "";
    public string ContentType { get; set; } = "";
    public long FileSize { get; set; }
    public string FilePath {get;set;} = "";
}