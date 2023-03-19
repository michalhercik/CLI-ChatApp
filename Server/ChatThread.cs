using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ChatApp;

public class ChatThread : IPoolElement
{
    public string Name { get; private set; }
    public int Id { get; set; }

    private List<ChatClient> _members = new List<ChatClient>();

    public void Set(int id)
    {
        Id = id;
    }

    public bool IsSet()
    {
        return Id >= 0;
    }

    public void Unset()
    {
        Id = -1;
    }

    public void AddMember(ChatClient client)
    {
        _members.Add(client);
    }

    public void RemoveMember(ChatClient client)
    {
        _members.Remove(client); 
    }

    public async Task SendToAll(byte[] data)
    {
        var tasks = new List<Task>();
        foreach (var member in _members)
        {
            var task = Task.Run(() => member.Send(data));
            tasks.Add(task);
        }
        Task.WaitAll(tasks.ToArray());
    }

    public async Task SendToAll(string msg)
    {
        var data = Encoding.UTF8.GetBytes(msg);
        await SendToAll(data);
    }
}
