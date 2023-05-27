using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class LeaveThreadCommand : Command
{
    public static CommandCode Code => CommandCode.LeaveThread;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        var thread = sender.CurrentThread;
        if (thread is not null)
        {
            thread.RemoveMember(sender);
            sender.KickFromThread(thread);
            Response responseToThread = Response.Message(SystemMsg.LeaveThread(sender.Name));
            Task.Run(() => thread!.SendToAll(responseToThread));
        }
        Response responseToSender = new Response(request.Id, ResponseStatus.Success);
        Task.Run(() => sender.Send(responseToSender));
    }
}

