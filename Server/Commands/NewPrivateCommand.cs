using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatApp;

public sealed class NewPrivateCommand : Command
{
    public static CommandCode Code => CommandCode.NewPrivate;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status;
        if (request.Data is not null)
        {
            if (server.TryAddPrivateThread(request.Data, sender, out ChatThread? thread))
            {
                status = ResponseStatus.Success;
                sender.JoinThread(thread!);
            }
            else
            {
                status = ResponseStatus.ThreadLimit;
            }
        }
        else
        {
            status = ResponseStatus.MissingCommandParameter;
        }
        var response = Response.RequestResponse(request.Id, status);
        Task.Run(() => sender.Send(response));
    }
}

