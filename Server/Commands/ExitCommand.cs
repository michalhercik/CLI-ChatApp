using CommunicationProtocol;

namespace ChatApp;

public sealed class ExitCommand : Command
{
    public static CommandCode Code => CommandCode.Exit;
    public override void Invoke(ChatClient sender, Server server, Request request)
    {
        sender.Unset();
        sender.SendAsync(new Response(request.Id, ResponseStatus.Success)).Wait();
    }
}

