using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record NewThread : IReciever
{
    public short Id { get; set; }
    public string Name => "new";
    public CommandCode Command => CommandCode.NewThread;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("Success");
        }
    }
}

