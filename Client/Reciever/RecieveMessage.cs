using System;
using System.Text;
using System.Text.Json;
using CommunicationProtocol;

namespace ChatAppClient;

public record RecieveMessage : IReciever
{
    public short Id { get; set; }
    public string Name => "";
    public CommandCode Command => CommandCode.None;

    public void Recieve(Response response)
    {
        if (response.Data is not null)
        {
            Message msg = ((JsonElement)response.Data).Deserialize<Message>()!;
            StringBuilder sb = new();
            if (msg.From is not null)
            {
                sb.Append($"{msg.From}: ");
            }
            sb.Append(msg.Text);
            Console.WriteLine(sb.ToString());
        }
    }
}

