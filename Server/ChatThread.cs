using System.Threading.Tasks;
using System.Collections.Generic;
using CommunicationProtocol;

namespace ChatApp;

public class ChatThread : IPoolElement
{
    public string? Name { get; set; }
    public int Id { get; set; } = -1;
    public ChatClient? Admin { get; set; }
    public bool IsPrivate => _private;
    private bool _private = true;

    public int MembersCount => _members.Count;

    private ConcurrentHashSet<ChatClient> _members = new();
    private ConcurrentHashSet<ChatClient> _whitelist = new();

    public void Set(int id)
    {
        _whitelist.Clear();
        _members.Clear();
        Id = id;
    }

    public bool IsSet()
    {
        return Id >= 0;
    }

    public void Unset()
    {
        foreach (var member in _members)
        {
            member.KickFromThread(this);
        }
        _private = true;
        Id = -1;
    }

    public void SetPrivate(ChatClient admin)
    {
        _private = true;
        Admin = admin;
        AddToWhitelist(admin);
        AddMember(admin);
    }

    public void SetPublic()
    {
        _private = false;
    }

    public void AddMember(ChatClient client)
    {
        _members.Add(client);
    }

    public void RemoveMember(ChatClient client)
    {
        _members.Remove(client);
    }

    public void AddToWhitelist(ChatClient client)
    {
        _whitelist.Add(client);
    }

    public void RemoveFromWhitelist(ChatClient client)
    {
        _whitelist.Remove(client);
        if (client == Admin)
        {
            _private = false;
        }
    }

    public void Kick(ChatClient client)
    {
        RemoveFromWhitelist(client);
        RemoveMember(client);
        client.KickFromThread(this);
    }

    public bool IsOnWhitelist(ChatClient client)
    {
        if (IsPrivate)
        {
            return _whitelist.Contains(client);
        }
        return true;
    }

    public async Task SendToAll(Response data)
    {
        var tasks = new List<Task>();
        foreach (var member in _members)
        {
            var task = Task.Run(() => member.Send(data));
            tasks.Add(task);
        }
        await Task.WhenAll(tasks.ToArray());
    }

    public void SendToAllExcept(ChatClient client, Response data)
    {
        var tasks = new List<Task>();
        foreach (var member in _members)
        {
            if (member.Name != client.Name)
            {
                var task = Task.Run(() => member.Send(data));
                tasks.Add(task);
            }
        }
        Task.WaitAll(tasks.ToArray());
    }

    public override string ToString()
    {
        return $"{Id}:{Name} ({_members.Count})";
    }
}
