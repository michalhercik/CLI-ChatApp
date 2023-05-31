using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class UnknownCommand : Command
{
    public override async Task Invoke(ChatClient sender, Server server, Request request)
    {
        var response = new Response(request.Id, ResponseStatus.UnknownCommand);
        await sender.SendAsync(response);
    }
}

