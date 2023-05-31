using System.Threading.Tasks;
using System.Linq;
using CommunicationProtocol;

namespace ChatApp;

public sealed class ListThreadsCommand : Command
{
    public static CommandCode Code => CommandCode.ListThreads;
    public override async Task Invoke(ChatClient sender, Server server, Request request)
    {
        var threads = server.GetThreads(sender)
            .Select(thread => new ThreadInfo(thread.Name!, (ushort)thread.MembersCount))
            .ToList();

        Response response = new Response(request.Id, ResponseStatus.Success, threads);

        await sender.SendAsync(response);
    }
}
