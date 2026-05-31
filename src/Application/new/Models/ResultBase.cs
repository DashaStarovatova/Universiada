namespace Application.Models;

public abstract class ResultBase;

public class SuccessResult : ResultBase;

public class SuccessResult<TData> : SuccessResult
{
    public required TData Data { get; init; }
}

public class FailResult : ResultBase
{
    public required string Description { get; init; }
}
