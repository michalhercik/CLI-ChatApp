using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public abstract class Command
{
    public abstract Task Invoke(ChatClient sender, Server server, Request request);
}

