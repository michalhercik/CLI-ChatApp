using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class ExitCommand : Command
{
    public static CommandCode Code => CommandCode.Exit;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        Task.Run(() => sender.Send(new Response(request.Id, ResponseStatus.Success)));
        sender.Unset();
    }
}

