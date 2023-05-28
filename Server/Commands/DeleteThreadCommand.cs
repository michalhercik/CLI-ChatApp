using CommunicationProtocol;

namespace ChatApp;

public sealed class DeleteThreadCommand : Command
{
    public static CommandCode Code => CommandCode.DeleteThread;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        ResponseStatus status = ResponseStatus.Success;
        if (request.Data is not null)
        {
            if (!TryDeleteThreadByName(sender, server, request))
            {
                status = ResponseStatus.UnknownThread;
            }
        }
        else if (sender.IsInThread())
        {
            DeleteCurrentThread(sender, server, request);
        }
        else
        {
            status = ResponseStatus.MissingCommandParameter;
        }
        Response response = new Response(request.Id, status);
        sender.SendAsync(response).Wait();
    }

    private void DeleteCurrentThread(ChatClient sender, Server server, Request request)
    {
        sender.CurrentThread?.Unset();
    }

    private bool TryDeleteThreadByName(ChatClient sender, Server server, Request request)
    {
        var thread = server.GetThreads(sender)
            .Find(thread => thread.Name == request.Data);
        if (thread is not null)
        {
            thread.Unset();
            return true;
        }
        return false;
    }
}

