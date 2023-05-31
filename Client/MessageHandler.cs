using System;
using System.Text;
using System.Threading.Tasks;
using CommunicationProtocol;

namespace ChatAppClient;

public class MessageHandler : IMessageHandler
{
    private RecieverCollection _modules;

    public MessageHandler()
    {
        _modules = new();
        _modules.Add(new RecieveMessage());
        _modules.Add(new SendMessage());
        _modules.Add(new NewThread());
        _modules.Add(new JoinThread());
        _modules.Add(new ListThreads());
        _modules.Add(new DeleteThread());
        _modules.Add(new LeaveThread());
        _modules.Add(new Exit());
        _modules.Add(new Info());
        _modules.Add(new NewPrivate());
        _modules.Add(new Add());
        _modules.Add(new Kick());
    }

    public async Task RecieveAsync(Response response)
    {
        if (_modules.TryGet(response.RequestId, out IReciever module))
        {
            await Task.Run(() => module.Recieve(response));
        }
        else
        {
            Console.WriteLine("Error: recieved response with unknown request id");
        }
    }

    public async Task SendAsync(Client client, string data)
    {
        if (data[0] == '/')
        {
            var (name, argument) = Parse(data);
            argument = argument.Length == 0 ? null : argument;
            if (_modules.TryGet(name, out IReciever module))
            {
                if (name == "exit")
                {
                    client.Stop();
                }
                var request = new Request(module.Id, module.Command, argument);
                await client.SendAsync(request);
            }
            else
            {
                Console.WriteLine("Error: unknown command");
            }
        }
        else
        {
            _modules.TryGet("message", out IReciever reciever);
            var request = new Request(reciever.Id, reciever.Command, data);
            await client.SendAsync(request);
        }
    }

    private (string, string) Parse(string data)
    {
        var name = new StringBuilder();
        var argument = new StringBuilder();
        int i = 1;
        for (; i < data.Length; ++i)
        {
            if (data[i] == ' ') break;
            name.Append(data[i]);
        }
        ++i;
        for (; i < data.Length; ++i)
        {
            argument.Append(data[i]);
        }
        return (name.ToString(), argument.ToString());
    }
}
