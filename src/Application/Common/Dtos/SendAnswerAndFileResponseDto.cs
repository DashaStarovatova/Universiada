using Domain;

public class SendAnswerAndFileResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public List<Result>? Results { get; set; }  // для успеха
    public Guid AnswerId { get; set; }  // для долгих операций
    public string? ErrorCode { get; set; }
    public bool IsFileLoaded {get;set;} // True - если файл загружен
}