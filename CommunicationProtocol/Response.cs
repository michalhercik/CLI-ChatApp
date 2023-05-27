using System.Text.Json.Serialization;

namespace CommunicationProtocol;

public interface IResponse
{
    public short RequestId { get; set; }
    public ResponseStatus Status { get; set; }
    public object? Data { get; set; }
}

public record Response : IResponse
{
    [JsonIgnore]
    public static readonly short MessageId = 0;
    public short RequestId { get; set; }
    public ResponseStatus Status { get; set; }
    public object? Data { get; set; }
    [JsonIgnore]
    public bool IsMessage => RequestId == MessageId;

    public Response() { }

    public Response(short requestId, ResponseStatus status, object data)
    {
        RequestId = requestId;
        Status = status;
        Data = data;
    }

    public Response(short requestId, ResponseStatus status)
    {
        RequestId = requestId;
        Status = status;
        Data = null;
    }

    public static Response Message(string from, string text)
        => new Response(MessageId, ResponseStatus.Success, new Message(from, text));

    public static Response Message(string text)
        => new Response(MessageId, ResponseStatus.Success, new Message(text));

    public static Response RequestResponse(short requestId, ResponseStatus status)
        => new Response(requestId, status);

    public static Response Handshake(ResponseStatus status)
        => new Response(-1, status);
}
