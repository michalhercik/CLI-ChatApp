using System.Text;

namespace Logger;

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    private static ConsoleLogger? _instance;

    private ConsoleLogger() { }

    public void Log(string message)
    {
        StringBuilder sb = new();
        sb.Append(DateTime.Now);
        sb.Append(" - ");
        sb.Append(message);
        Console.WriteLine(sb.ToString());
    }

    public static ConsoleLogger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ConsoleLogger();
        }
        return _instance;
    }
}
