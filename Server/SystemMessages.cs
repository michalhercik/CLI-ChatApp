namespace ChatApp;

public static class SystemMsg
{
    public static string JoinThread(string ClientName)
        => $"{ClientName} joined!";

    public static string LeaveThread(string ClientName)
        => $"{ClientName} left!";

    public static string KickFromThread(string threadName)
        => $"Kicked from {threadName}!";
}
