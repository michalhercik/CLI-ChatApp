using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatApp;

public class ChatClient : IPoolElement
{
    public string IpAddress => _client.Client.RemoteEndPoint.ToString();

    private TcpClient _client;
    private int _id;

    public ChatClient()
    {
    }

    public void Set(int id)
    {
        _id = id;
    }

    public bool IsSet()
    {
        return _client != null;
    }

    public void Unset()
    {
        _client = null;
    }

    public void SetTcpClient(TcpClient client)
    {
        _client = client;
    }

    public async Task Listen()
    {
        var buffer = new byte[1_024];
        while (true)
        {
            var received = await _client.Client.ReceiveAsync(buffer, SocketFlags.None);
            var msg = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"{IpAddress}: {msg}");
        }
    }

    public async Task Send(byte[] data)
    {
        await _client.Client.SendAsync(data, SocketFlags.None);
    }

    public async Task Send(string msg)
    {
        var data = Encoding.UTF8.GetBytes(msg);
        await Send(data);
    }
}
