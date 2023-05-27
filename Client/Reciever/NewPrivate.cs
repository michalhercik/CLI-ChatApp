using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record NewPrivate : IReciever
{
    public short Id { get; set; }
    public string Name => "newprivate";
    public CommandCode Command => CommandCode.NewPrivate;

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

