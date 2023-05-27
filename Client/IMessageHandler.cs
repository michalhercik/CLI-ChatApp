using CommunicationProtocol;

namespace ChatAppClient;

public interface IMessageHandler
{
    void Recieve(Response response);
    void Send(Client client, string request);
}

