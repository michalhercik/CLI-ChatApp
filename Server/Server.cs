using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Logger;

namespace ChatApp;

public class Server
{
    private string? _password;
    private Pool<ChatClient> _clients;
    private Pool<ChatThread> _threads;
    private TcpListener _listener;
    private ILogger _logger = ConsoleLogger.GetInstance();
    private readonly bool _logging = true;


    public Server(IPAddress ipAddress, int port, string? password)
    {
        _listener = new TcpListener(ipAddress, port);
        const int maxClients = 100;
        _clients = new Pool<ChatClient>(maxClients);
        const int maxThreads = 20;
        _threads = new Pool<ChatThread>(maxThreads);
        _password = password;
    }

    public async Task Run()
    {
        _listener.Start();
        Log("Server is running...");
        await Task.Run(AcceptClients);
    }

    public bool Authenticate(string? password)
        => password == _password;

    public bool TryGetClient(string clientName, out ChatClient? client)
    {
        var activeClients = _clients.GetActive();
        client = Array.Find(activeClients,
                c => c.Name == clientName);
        return client is not null;
    }

    public bool TryAddThread(string name, out ChatThread? thread)
    {
        if (_threads.TryGetFree(out thread))
        {
            Log($"Thread {name} is created");
            thread!.Name = name;
            thread!.SetPublic();
            return true;
        }
        return false;
    }

    public bool TryAddPrivateThread(string name, ChatClient admin, out ChatThread? thread)
    {
        if (_threads.TryGetFree(out thread))
        {
            Log($"Private thread {name} is created with admin {admin.Name}");
            thread!.Name = name;
            thread!.SetPrivate(admin);
            return true;
        }
        return false;
    }

    public bool TryGetThread(string threadName, ChatClient client, out ChatThread? thread)
    {
        var activeThreads = _threads.GetActive();
        thread = Array.Find(activeThreads,
                t => t.Name == threadName && t.IsOnWhitelist(client));
        return thread is not null;
    }

    public List<ChatThread> GetThreads(ChatClient client)
    {
        return _threads.GetActive()
            .Where(t => t.IsOnWhitelist(client))
            .ToList();
    }

    private async Task AcceptClients()
    {
        Log("accepting clients...");
        while (true)
        {
            var tcpClient = await _listener.AcceptTcpClientAsync();
            if (_clients.TryGetFree(out ChatClient? client))
            {
                client!.SetTcpClient(tcpClient);
                Log($"New connection from: {client.IpAddress}");
                var toSuppressWarning = Task.Run(() => client.Listen(this));
            }
            else
            {
                tcpClient.Close();
            }
        }
    }

    private void Log(string message)
    {
        if (_logging)
        {
            _logger.Log(message);
        }
    }
}
