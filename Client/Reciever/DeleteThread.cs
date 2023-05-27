using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record DeleteThread : IReciever
{
    public short Id { get; set; }
    public string Name => "del";
    public CommandCode Command => CommandCode.DeleteThread;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("Thread deleted!");
        }
    }
}

