using CommunicationProtocol;

namespace ChatApp;

public sealed class UnknownCommand : Command
{
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        var response = new Response(request.Id, ResponseStatus.UnknownCommand);
        sender.SendAsync(response).Wait();
    }
}

