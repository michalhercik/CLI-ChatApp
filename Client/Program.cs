using System;
using System.Net;
using System.Threading.Tasks;
using CommandLineParser;
using CustomOptionParser;

namespace ChatAppClient;

class Program
{
    record Options
    {
        public IPAddress? IpAddress { get; set; }
        public int Socket { get; set; }
        public string? Nickname { get; set; }
        public string? Password { get; set; }
    }

    static CommandLineParser<Options> GetParser()
    {
        CommandLineParser<Options> parser = new();

        parser.AddOption<int>()
            .AddLongAlias("socket")
            .AddShortAlias('s')
            .SetPropertyPicker(o => o.Socket)
            .SetValidator(socket => socket > 0)
            .SetDefault(5555);

        parser.AddOption<IPAddress>()
            .AddLongAlias("ipAddress")
            .AddLongAlias("ip")
            .SetPropertyPicker(o => o.IpAddress!);

        parser.AddOption<string>()
            .AddLongAlias("nickname")
            .AddShortAlias('n')
            .SetPropertyPicker(o => o.Nickname!)
            .SetDefault("");

        parser.AddOption<string>()
            .AddLongAlias("password")
            .AddShortAlias('p')
            .SetPropertyPicker(o => o.Password!)
            .SetDefault("");

        return parser;
    }

    static async Task Main(string[] args)
    {
        var parser = GetParser();

        if (parser.TryParse(args, OptionParser.Create()))
        {
            var r = parser.Result;
            var client = new Client();
            Console.CancelKeyPress += delegate
            {
                client.Disconnect().Wait();
            };
            await client.Run(r.IpAddress!, r.Socket, r.Nickname!, r.Password);
        }
        else
        {
            foreach (var e in parser.Errors)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
