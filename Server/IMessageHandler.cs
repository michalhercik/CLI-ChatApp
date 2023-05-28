using System.Threading.Tasks;

namespace ChatApp;

public interface IMessageHandler
{
    public Task HandleAsync(ChatClient sender, Server server, byte[] data, int length);
}
