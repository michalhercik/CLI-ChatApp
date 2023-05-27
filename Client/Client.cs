using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text.Json;
using CommunicationProtocol;

namespace ChatAppClient;

public class Client
{
    private bool _running = true;
    private TcpClient _client = new TcpClient();
    private IMessageHandler _moduleHandler = new MessageHandler();

    public async Task Run(IPAddress ip, int socket, string nick, string? password)
    {
        try
        {
            await _client.ConnectAsync(ip, socket);
        }
        catch (SocketException)
        {
            Console.WriteLine("Could not connect to server.");
            Console.WriteLine("terminating...");
            return;
        }

        if (await Handshake(nick, password))
        {
            var socketListening = Task.Run(ListenSocket);
            var consoleListening = Task.Run(ListenConsole);
            Task.WaitAll(socketListening, consoleListening);
        }
        else
        {
            Console.WriteLine("Handshake failed.");
            Stop();
        }
    }

    private async Task<bool> Handshake(string nick, string? password)
    {
        var request = new SetupRequest(nick, password);
        await Send(request);
        var buffer = new byte[1_024];
        try
        {
            var response = await Receive();
            if (response is not null && response.Status.Ok)
            {
                return true;
            }
            Console.WriteLine(response?.Status);
        }
        catch (SocketException)
        {
            Console.WriteLine("Server is full.");
        }
        return false;
    }

    private async Task ListenSocket()
    {
        var buffer = new byte[1_024];
        while (_running)
        {
            try
            {
                var response = await Receive();
                if (!_running) break;
                if (response is not null)
                {
                    _moduleHandler.Recieve(response);
                }
                else
                {
                    Console.WriteLine("Recieved invalid message.");
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("server disconnected.");
                Console.WriteLine("Press any key to exit...");
                Stop();
            }
        }
    }

    private void ListenConsole()
    {
        while (_running)
        {
            var data = Console.ReadLine();
            if (data is not null && _running)
            {
                try
                {
                    _moduleHandler.Send(this, data);
                }
                catch (SocketException)
                {
                    Console.WriteLine("server disconnected.");
                    Stop();
                }
            }
        }
    }

    private async Task<Response?> Receive()
    {
        var buffer = new byte[1_024];
        var received = await _client.Client.ReceiveAsync(buffer, SocketFlags.None);
        if (received == 0) return null;
        var msg = Encoding.UTF8.GetString(buffer, 0, received);
        return JsonSerializer.Deserialize<Response>(msg);
    }

    public async Task Send(object data)
    {
        var serialized = JsonSerializer.Serialize(data);
        var encoded = Encoding.UTF8.GetBytes(serialized);
        await _client.Client.SendAsync(encoded, SocketFlags.None);
    }

    public async Task Disconnect()
    {
        if (_running)
        {
            var request = new Request(-1, CommandCode.Exit);
            await Send(request);
            _running = false;
        }
    }

    public void Stop()
    {
        if (_running)
        {
            _running = false;
        }
    }
}

