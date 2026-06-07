namespace Shared.Response;

public class ApiResponse<T>
{
    public bool Status { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }

    public static ApiResponse<T> Success(T data, string message = "Success") =>
        new() { Status = true, Message = message, Data = data };

    public static ApiResponse<T> Fail(string message) =>
        new() { Status = false, Message = message, Data = default };
}
