using CommunicationProtocol;

namespace ChatApp;

public sealed class NewThreadCommand : Command
{
    public static CommandCode Code => CommandCode.NewThread;

    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status;
        if (request.Data is not null)
        {
            if (server.TryAddThread(request.Data, out ChatThread? thread))
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
        sender.SendAsync(response).Wait();
    }
}

