using System.Threading.Tasks;
using System.Collections.Generic;
using CommunicationProtocol;

namespace ChatApp;

public sealed class LeaveThreadCommand : Command
{
    public static CommandCode Code => CommandCode.LeaveThread;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        List<Task> messages = new();
        var thread = sender.CurrentThread;
        if (thread is not null)
        {
            thread.RemoveMember(sender);
            sender.KickFromThread(thread);
            Response responseToThread = Response.Message(SystemMsg.LeaveThread(sender.Name));
            messages.Add(
                    Task.Run(() => thread.SendToAll(responseToThread))
                    );
        }
        Response responseToSender = new Response(request.Id, ResponseStatus.Success);
        messages.Add(
                Task.Run(() => sender.SendAsync(responseToSender))
                );
        Task.WaitAll(messages.ToArray());
    }
}

