using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

public class ChatAppClient
{
    private TcpClient _client = new TcpClient();

    public async Task Run(IPAddress ip, int socket)
    {
        await _client.ConnectAsync(ip, socket);
        var socketListening = Task.Run(ListenSocket);
        var consoleListening = Task.Run(ListenConsole);
        Task.WaitAll(socketListening, consoleListening);
    }

    private async Task ListenSocket()
    {
        var buffer = new byte[1_024];
        while (true)
        {
            var received = await _client.Client.ReceiveAsync(buffer, SocketFlags.None);
            var msg = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"receive: {msg}");
            Send("Thank you!");
        }
    }

    private async Task ListenConsole()
    {
        while (true)
        {
            var msg = Console.ReadLine();
            Send(msg);
        }
    }

    private async Task Send(byte[] data)
    {
        await _client.Client.SendAsync(data, SocketFlags.None);
    }

    private async Task Send(string msg)
    {
        var data = Encoding.UTF8.GetBytes(msg);
        await Send(data);
    }
}

class Program
{
    static async Task Main()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int socket = 5555;

        var client = new ChatAppClient();
        await client.Run(ip, socket);
    }
}
