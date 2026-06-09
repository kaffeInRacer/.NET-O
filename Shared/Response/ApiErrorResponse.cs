using System.Text.Json.Serialization;

namespace Shared.Response;

public class ApiErrorResponse
{
    [JsonPropertyName("status")]
    public bool Status { get; init; } = false;
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
    [JsonPropertyName("errors")]
    public IEnumerable<string>? Errors { get; init; }

    public static ApiErrorResponse From(string message, IEnumerable<string>? errors = null) =>
        new() { Status = false, Message = message, Errors = errors };
}