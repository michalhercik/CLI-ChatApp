using System.Net;
using System.Reflection;
using CommandLineParser;
using CommandLineParser.OptionParser;
using CommandLineParser.Error;

namespace CustomOptionParser;

public class IpParser : IChainOptionParserElement, IOptionParser
{
    public Result<T> ParseNext<T>(string[] args, ref int offset)
    {
        var parseMethod = typeof(T).GetMethod("Parse", new[] { typeof(string) })!;
        try
        {
            T? value = (T)parseMethod.Invoke(null, new object[] { args[offset] })!;
            if (value is not null)
            {
                return new Result<T>(value);
            }
            return new Result<T>(
                    new InvalidFormatParserError($"{args[offset]} at argument position {offset}")
                    );
        }
        catch (TargetInvocationException)
        {
            return new Result<T>(
                    new InvalidFormatParserError($"{args[offset]} at argument position {offset}")
                    );
        }
    }

    public bool CanParse<T>()
        => typeof(T) == typeof(IPAddress);
}

public static class OptionParser
{
    public static IOptionParser Create()
    {
        return new ChainOptionParser(
                new SimpleOptionParser(),
                new ChainOptionParser(new IpParser())
                );
    }
}
