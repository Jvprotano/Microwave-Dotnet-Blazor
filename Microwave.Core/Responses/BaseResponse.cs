using System.Text.Json.Serialization;

namespace Microwave.Core.Responses;

public class BaseResponse<T>
{
    [JsonConstructor]
    public BaseResponse()
    {
    }
    public BaseResponse(T? data, string? message = null)
    {
        Data = data;
        Message = message;
    }

    public string? Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}