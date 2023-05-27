using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record JoinThread : IReciever
{
    public short Id { get; set; }
    public string Name => "join";
    public CommandCode Command => CommandCode.JoinThread;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("Joined!");
        }
    }
}

