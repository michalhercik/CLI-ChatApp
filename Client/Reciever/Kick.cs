using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record Kick : IReciever
{
    public short Id { get; set; }
    public string Name => "kick";
    public CommandCode Command => CommandCode.Kick;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("User kicked!");
        }
    }
}

