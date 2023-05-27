using System;
using System.Text;
using System.Text.Json;
using CommunicationProtocol;

namespace ChatAppClient;

public record Info : IReciever
{
    public short Id { get; set; }
    public string Name => "info";
    public CommandCode Command => CommandCode.Info;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine(Parse(response));
        }
    }

    private string Parse(Response response)
    {
        var info = ((JsonElement)response.Data!).Deserialize<ClientInfo>();
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.AppendLine("Info:");
        sb.AppendLine($"  name: {info!.Name}");
        sb.AppendLine($"  Current thread: {info.Thread ?? "none"}");
        sb.AppendLine();
        return sb.ToString();
    }
}

