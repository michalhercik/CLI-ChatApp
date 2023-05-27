using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class AddCommand : Command
{
    public static CommandCode Code => CommandCode.Add;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status;
        var thread = sender.CurrentThread;
        if (thread is null)
        {
            status = ResponseStatus.NoCurrentThread;
        }
        else if (thread.Admin != sender)
        {
            status = ResponseStatus.NoRights;
        }
        else if (request.Data is not null)
        {
            if (server.TryGetClient(request.Data, out ChatClient? client))
            {
                status = ResponseStatus.Success;
                thread.AddToWhitelist(client!);
            }
            else
            {
                status = ResponseStatus.UnknownClient;
            }
        }
        else
        {
            status = ResponseStatus.MissingCommandParameter;
        }
        Response responseToSender = new Response(request.Id, status);
        Task.Run(() => sender.Send(responseToSender));
    }
}

