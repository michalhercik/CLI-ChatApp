using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record LeaveThread : IReciever
{
    public short Id { get; set; }
    public string Name => "leave";
    public CommandCode Command => CommandCode.LeaveThread;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("Left!");
        }
    }
}

