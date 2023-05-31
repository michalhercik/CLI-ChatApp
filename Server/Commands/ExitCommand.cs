using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class ExitCommand : Command
{
    public static CommandCode Code => CommandCode.Exit;
    public override async Task Invoke(ChatClient sender, Server server, Request request)
    {
        sender.Unset();
        await sender.SendAsync(new Response(request.Id, ResponseStatus.Success));
    }
}

