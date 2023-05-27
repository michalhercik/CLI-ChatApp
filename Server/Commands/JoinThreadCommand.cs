using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class JoinThreadCommand : Command
{
    public static CommandCode Code => CommandCode.JoinThread;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status;
        if (request.Data is not null && server.TryGetThread(request.Data, sender, out ChatThread? thread))
        {
            status = ResponseStatus.Success;
            sender.JoinThread(thread!);
            Response responseToThread = Response.Message(SystemMsg.JoinThread(sender.Name));
            Task.Run(() => thread!.SendToAllExcept(sender, responseToThread));
        }
        else if (request.Data is not null)
        {

            status = ResponseStatus.UnknownThread;
        }
        else
        {
            status = ResponseStatus.MissingCommandParameter;
        }
        Response responseToSender = new Response(request.Id, status);
        Task.Run(() => sender.Send(responseToSender));
    }
}

