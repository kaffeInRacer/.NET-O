using System.Text.Json.Serialization;

namespace Shared.Response;

public class PagedResponse<T>
{
    [JsonPropertyName("status")]
    public bool Status { get; init; } = true;
    [JsonPropertyName("message")]
    public string Message { get; init; } = "Success";
    [JsonPropertyName("page")]
    public int Page { get; init; }
    [JsonPropertyName("limit")]
    public int Limit { get; init; }
    [JsonPropertyName("total_data")]
    public int TotalData { get; init; }
    [JsonPropertyName("total_pages")]
    public int TotalPages => (int)Math.Ceiling((double)TotalData / Limit);
    [JsonPropertyName("next")]
    public bool HasNext => Page < TotalPages;
    [JsonPropertyName("previous")]
    public bool HasPrevious => Page > 1;
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; init; } = [];

    public static PagedResponse<T> Create(IEnumerable<T> data, int page, int limit, int totalData, string message = "Success") =>
        new()
        {
            Status = true,
            Message = message,
            Page = page,
            Limit = limit,
            TotalData = totalData,
            Data = data
        };
}