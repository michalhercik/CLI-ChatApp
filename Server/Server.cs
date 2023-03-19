using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatApp;

public class Server
{
    private Pool<ChatClient> _clients;
    private Pool<ChatThread> _threads;
    private TcpListener _listener;

    public Server(IPAddress ipAddress, int port)
    {
        _listener = new TcpListener(ipAddress, port);
        const int maxClients = 100;
        _clients = new Pool<ChatClient>(maxClients);
        const int maxThreads = 20;
        _threads = new Pool<ChatThread>(maxThreads);
    }

    public async Task Run()
    {
        _listener.Start();
        await Task.Run(AcceptClients);
    }

    private async Task AcceptClients()
    {
        Console.WriteLine("AcceptClients()");
        while (true)
        {
            var tcpClient = await _listener.AcceptTcpClientAsync();
            var client = _clients.GetFree();
            client.SetTcpClient(tcpClient);
            Console.WriteLine($"New connection from: {client.IpAddress}");
            client.Send("Welcome to the server!");
            Task.Run(client.Listen);
        }    
    }
}
