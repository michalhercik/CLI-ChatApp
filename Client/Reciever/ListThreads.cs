using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using CommunicationProtocol;

namespace ChatAppClient;

public record ListThreads : IReciever
{
    public short Id { get; set; }
    public string Name => "threads";
    public CommandCode Command => CommandCode.ListThreads;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else if (response.Data is not null)
        {
            Console.WriteLine("\nThreads:");
            Console.WriteLine(ToString(ToList(response)));
            Console.WriteLine();
        }
    }

    private List<ThreadInfo> ToList(Response response)
    {
        return ((JsonElement)response.Data!).Deserialize<List<ThreadInfo>>()!;
    }

    private string ToString(List<ThreadInfo> list)
    {
        var str = new StringBuilder();
        foreach (var e in list)
        {
            str.Append($"   {e.Name} ({e.MembersCount})");
            str.Append('\n');
        }
        if (str.Length > 0)
        {
            --str.Length;
        }
        return str.ToString();
    }
}
