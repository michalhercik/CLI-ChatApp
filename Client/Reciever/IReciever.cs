using CommunicationProtocol;

namespace ChatAppClient;

public interface IReciever
{
    public short Id { get; set; }
    public string Name { get; }
    public CommandCode Command { get; }

    void Recieve(Response response);
}

