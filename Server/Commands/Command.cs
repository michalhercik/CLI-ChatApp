using CommunicationProtocol;

namespace ChatApp;

public abstract class Command
{
    public abstract void Invoke(ChatClient sender, Server server, Request request);
}

