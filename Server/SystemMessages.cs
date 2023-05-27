using CommunicationProtocol;

namespace ChatApp;

public static class SystemMsg
{
    public static string NewThreadResponse(string threadName)
        => $"Thread {threadName} sucessfully created!";

    public static string JoinThreadResponse(string threadName)
        => $"Thread {threadName} sucessfully joined!";

    public static string JoinThread(string ClientName)
        => $"{ClientName} joined!";

    public static string LeaveThreadResponse(string threadName)
        => $"You left thread {threadName}";

    public static string LeaveThread(string ClientName)
        => $"{ClientName} left!";

    public static string NoCurrentThreadResponse()
    {
        return "You have to join a thread to send messages.";
        // return new Response<string>() { RequestId = request.Id, Status = new ResponseStatus(0), Data =  msg};
    }

    public static string UnknownCommandResponse()
        => "Unknown command.";

    public static string NoCurrentThreadOrArgResponse()
        => "You have to join a thread you want to delete or pass a name of the thread as parameter.";

    public static string DeleteThreadResponse(string threadName)
        => $"Thread {threadName} is succesfully deleted";
}
