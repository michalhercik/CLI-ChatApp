using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class InfoCommand : Command
{
    public static CommandCode Code => CommandCode.Info;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        var data = new ClientInfo(sender.Name, sender.CurrentThread?.Name);
        Response responseToSender = new Response(request.Id, ResponseStatus.Success, data);
        Task.Run(() => sender.Send(responseToSender));
    }
}

