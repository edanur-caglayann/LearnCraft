namespace LearnCraftt.Application.Common;

public class ServiceResult<T>
{
    public bool Success { get;private set; }
    public string Message { get; private set; }
    
    // string, inr, dto, list, entity, bool vs her sey gonderebilrisin object ile
    public T? Data { get; private set; }
    
    public static ServiceResult<T> SuccessResult(T data)
        => new ServiceResult<T> { Success = true, Data = data };

    public static ServiceResult<T> FailResult(string message)
        => new ServiceResult<T> { Success = false, Message = message };

}