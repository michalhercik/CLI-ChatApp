using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record SendMessage : IReciever
{
    public short Id { get; set; }
    public string Name => "message";
    public CommandCode Command => CommandCode.SendMessage;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
    }
}
