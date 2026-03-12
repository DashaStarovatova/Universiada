using MediatR;

// RunMatlabCommand.cs
public class RunMatlabCommand : IRequest
{

    // Написать всё, что нужно для запуска MATLAB
    // важен порядок параметров

    // Опциональные параметры
    public string? MatlabExeFile { get; set; }
    public string? WorkDirectory { get; set; }
    public string? ScriptName { get; set; }

    // Обязательные параметры
    public double KeyRate { get; set; } 
    public int IsRefresh { get; set; }  
    public Guid TeamId { get; set; }
    public Guid AnswerId {get;set;}
}