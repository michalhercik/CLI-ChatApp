namespace CommunicationProtocol;

public interface IRequest
{
    public short Id { get; }
    public CommandCode Command { get; }
    public string? Data { get; }
}

public class Request : IRequest
{
    public short Id { get; set; }
    public CommandCode Command { get; set; }
    public string? Data { get; set; }

    public Request() { }

    public Request(short id, CommandCode command, string? args)
    {
        Id = id;
        Command = command;
        Data = args;
    }

    public Request(short id, CommandCode command)
    {
        Id = id;
        Command = command;
        Data = null;
    }
}

