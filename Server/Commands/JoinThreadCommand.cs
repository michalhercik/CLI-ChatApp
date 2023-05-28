using System.Threading.Tasks;
using System.Collections.Generic;
using CommunicationProtocol;

namespace ChatApp;

public sealed class JoinThreadCommand : Command
{
    public static CommandCode Code => CommandCode.JoinThread;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status;
        List<Task> messages = new();
        if (request.Data is not null && server.TryGetThread(request.Data, sender, out ChatThread? thread))
        {
            status = ResponseStatus.Success;
            sender.JoinThread(thread!);
            Response responseToThread = Response.Message(SystemMsg.JoinThread(sender.Name));
            messages.Add(
                    Task.Run(() => thread!.SendToAllExcept(sender, responseToThread))
                    );
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
        messages.Add(
                sender.SendAsync(responseToSender)
                );
        Task.WaitAll(messages.ToArray());
    }
}

