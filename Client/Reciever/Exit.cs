using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record Exit : IReciever
{
    public short Id { get; set; }
    public string Name => "exit";
    public CommandCode Command => CommandCode.Exit;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("Exiting...");
        }
    }
}

