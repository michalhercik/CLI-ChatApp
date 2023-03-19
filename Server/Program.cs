using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp;

class Program
{
    static async Task Main()
    {
        IPAddress ip = IPAddress.Any;
        int socket = 5555;

        Server server = new Server(ip, socket);
        await server.Run();
    }
}
