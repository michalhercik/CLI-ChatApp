using System.Linq;
using CommunicationProtocol;

namespace ChatApp;

public sealed class ListThreadsCommand : Command
{
    public static CommandCode Code => CommandCode.ListThreads;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        var threads = server.GetThreads(sender)
            .Select(thread => new ThreadInfo(thread.Name!, (ushort)thread.MembersCount))
            .ToList();

        Response response = new Response(request.Id, ResponseStatus.Success, threads);

        sender.SendAsync(response).Wait();
    }
}
