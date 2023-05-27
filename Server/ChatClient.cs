using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text.Json;
using CommunicationProtocol;
using Logger;

namespace ChatApp;

public class ChatClient : IPoolElement
{
    public string? IpAddress => _client?.Client.RemoteEndPoint?.ToString();
    public string Name => $"{_nick}_{_id}";
    public ChatThread? CurrentThread => _currentChatThread;

    private TcpClient? _client;
    private int _id;
    private string? _nick;
    private IMessageHandler _msgHandler;
    private ChatThread? _currentChatThread;
    private object _currentThreadLock = new Object();
    private ILogger _logger = ConsoleLogger.GetInstance();
    private bool _logging = true;

    public ChatClient()
    {
        _msgHandler = new MessageHandler();
    }

    public void SetNick(string nick)
    {
        _nick = nick;
    }

    public void Set(int id)
    {
        _id = id;
    }

    public void SetThread(ChatThread thread)
    {
        _currentChatThread = thread;
    }

    public bool IsSet()
    {
        return _client is not null;
    }

    public void Unset()
    {
        _currentChatThread?.RemoveMember(this);
        _currentChatThread = null;
        _client?.Close();
        _client = null;
        Log($"{Name} disconnected!");
    }

    public void SetTcpClient(TcpClient client)
    {
        _client = client;
    }

    public async Task Listen(Server server)
    {
        if (!await HandShake(server))
        {
            Log("Invalid handshake");
            return;
        }
        Log($"Successful handshake with name {Name} on address {IpAddress}");
        var buffer = new byte[1_024];
        while (IsSet())
        {
            var length = await (_client?.Client.ReceiveAsync(buffer, SocketFlags.None) ?? Task.FromResult(0));
            if (length > 0)
            {
                _msgHandler.Handle(this, server, buffer, length);
            }
        }
    }

    private async Task<bool> HandShake(Server server)
    {
        int length;
        var buffer = new byte[1_024];
        length = await _client!.Client.ReceiveAsync(buffer, SocketFlags.None);
        string message = Encoding.UTF8.GetString(buffer, 0, length);
        SetupRequest request = JsonSerializer.Deserialize<SetupRequest>(message)!;
        _nick = request.Nick;
        if (server.Authenticate(request.Password))
        {
            await Send(Response.Handshake(ResponseStatus.Success));
            return true;
        }
        else
        {
            await Send(Response.Handshake(ResponseStatus.InvalidPassword));
            return false;
        }
    }

    public async Task Send(Response data)
    {
        if (IsSet())
        {
            var serialized = JsonSerializer.Serialize(data);
            var encoded = Encoding.UTF8.GetBytes(serialized);
            await (_client?.Client.SendAsync(encoded, SocketFlags.None) ?? Task.CompletedTask);
        }
    }

    public void SendToThread(Response msg)
    {
        _currentChatThread?.SendToAllExcept(this, msg);
    }

    public void KickFromThread(ChatThread thread)
    {
        lock (_currentThreadLock)
        {
            if (_currentChatThread == thread)
            {
                _currentChatThread = null;
            }
        }
    }

    public void JoinThread(ChatThread thread)
    {
        lock (_currentThreadLock)
        {
            if (_currentChatThread is not null)
            {
                _currentChatThread.RemoveMember(this);
            }
            thread.AddMember(this);
            _currentChatThread = thread;
        }
    }

    public bool IsInThread()
    {
        lock (_currentThreadLock)
        {
            return _currentChatThread is not null;
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
