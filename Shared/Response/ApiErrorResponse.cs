namespace Shared.Response;

public class ApiErrorResponse
{
    public bool Status { get; init; } = false;
    public string Message { get; init; } = string.Empty;
    public IEnumerable<string>? Errors { get; init; }

    public static ApiErrorResponse From(string message, IEnumerable<string>? errors = null) =>
        new() { Status = false, Message = message, Errors = errors };
}