using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using CommunicationProtocol;

namespace ChatApp;

public sealed class MessageHandler : IMessageHandler
{
    private Dictionary<CommandCode, Command> _commands = new();
    private Command _default = new UnknownCommand();

    public MessageHandler()
    {
        _commands.Add(NewThreadCommand.Code, new NewThreadCommand());
        _commands.Add(ListThreadsCommand.Code, new ListThreadsCommand());
        _commands.Add(JoinThreadCommand.Code, new JoinThreadCommand());
        _commands.Add(LeaveThreadCommand.Code, new LeaveThreadCommand());
        _commands.Add(DeleteThreadCommand.Code, new DeleteThreadCommand());
        _commands.Add(ExitCommand.Code, new ExitCommand());
        _commands.Add(MessageCommand.Code, new MessageCommand());
        _commands.Add(InfoCommand.Code, new InfoCommand());
        _commands.Add(NewPrivateCommand.Code, new NewPrivateCommand());
        _commands.Add(AddCommand.Code, new AddCommand());
        _commands.Add(KickCommand.Code, new KickCommand());
    }

    public void Handle(ChatClient sender, Server server, byte[] data, int length)
    {
        string msg = Encoding.UTF8.GetString(data, 0, length);

        Request request = JsonSerializer.Deserialize<Request>(msg)!;
        if (_commands.TryGetValue(request.Command, out Command? command))
        {
            command.Invoke(sender, server, request);
        }
        else
        {
            _default.Invoke(sender, server, request);
        }
    }
}
