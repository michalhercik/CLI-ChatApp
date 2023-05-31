using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class MessageCommand : Command
{
    public static CommandCode Code => CommandCode.SendMessage;
    public override async Task Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status;
        var thread = sender.CurrentThread;
        if (thread is not null)
        {
            if (request.Data is not null)
            {
                Response responseToThread = Response.Message(sender.Name, request.Data);
                await Task.Run(() => thread.SendToAllExcept(sender, responseToThread));
                status = ResponseStatus.Success;
            }
            else
            {
                status = ResponseStatus.MissingCommandParameter;
            }
        }
        else
        {
            status = ResponseStatus.NoCurrentThread;
        }
        var response = new Response(request.Id, status);
        await sender.SendAsync(response);
    }
}

