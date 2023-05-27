using System;
using CommunicationProtocol;

namespace ChatAppClient;

public record Add : IReciever
{
    public short Id { get; set; }
    public string Name => "add";
    public CommandCode Command => CommandCode.Add;

    public void Recieve(Response response)
    {
        if (response.Status.Err)
        {
            Console.WriteLine(response.Status.Status);
        }
        else
        {
            Console.WriteLine("User added!");
        }
    }
}

