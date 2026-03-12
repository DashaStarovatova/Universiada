namespace Domain;

public class LoadedFile

{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; } // FOREIGN KEY → Team.Id
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
    public string ContentType { get; set; } = ""; // pdf, csv, rar etc.
    public long FileSize { get; set; }     // размер в байтах
    public DateTime CreatedAt {get ; set;}
    public DateTime CreatedDate { get; set; }



    private LoadedFile() {}
    
    public LoadedFile(Guid teamId, string name, string path, string contentType, long fileSize) : this()
    {
        Id = Guid.NewGuid();
        TeamId = teamId;
        Name = name;
        Path = path;
        ContentType = contentType;
        FileSize = fileSize;
        CreatedAt = DateTime.UtcNow;
        CreatedDate = DateTime.UtcNow.Date; 
    }

}