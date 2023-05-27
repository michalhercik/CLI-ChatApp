using System.Text.Json.Serialization;

namespace CommunicationProtocol;

public interface IResponseStatus
{
    public StatusCode Status { get; set; }
    public bool Ok { get; }
    public bool Err { get; }
}

public record struct ResponseStatus : IResponseStatus
{
    public static ResponseStatus Success => new ResponseStatus(StatusCode.Success);
    public static ResponseStatus NoCurrentThread => new ResponseStatus(StatusCode.NoCurrentThread);
    public static ResponseStatus UnknownCommand => new ResponseStatus(StatusCode.UnknownCommand);
    public static ResponseStatus MissingCommandParameter => new ResponseStatus(StatusCode.MissingCommandParameter);
    public static ResponseStatus ThreadLimit => new ResponseStatus(StatusCode.ThreadLimit);
    public static ResponseStatus UnknownThread => new ResponseStatus(StatusCode.UknownThread);
    public static ResponseStatus InvalidCommandParameter => new ResponseStatus(StatusCode.InvalidCommandParameter);
    public static ResponseStatus InvalidPassword => new ResponseStatus(StatusCode.InvalidPassword);
    public static ResponseStatus UnknownClient => new ResponseStatus(StatusCode.UnknownClient);
    public static ResponseStatus NoRights => new ResponseStatus(StatusCode.NoRights);

    public StatusCode Status { get; set; }
    [JsonIgnore]
    public bool Ok => Status == StatusCode.Success;
    [JsonIgnore]
    public bool Err => !Ok;

    public ResponseStatus() { }

    public ResponseStatus(StatusCode status)
    {
        Status = status;
    }
}

