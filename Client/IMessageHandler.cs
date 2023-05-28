using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatAppClient;

public interface IMessageHandler
{
    Task RecieveAsync(Response response);
    Task SendAsync(Client client, string request);
}

