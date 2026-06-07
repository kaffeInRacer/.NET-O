namespace Shared.Response;

public class PagedResponse<T>
{
    public bool Status { get; init; } = true;
    public string Message { get; init; } = "Success";
    public int Page { get; init; }
    public int Limit { get; init; }
    public int TotalData { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalData / Limit);
    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;
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