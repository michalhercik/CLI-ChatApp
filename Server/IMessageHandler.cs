namespace ChatApp;

public interface IMessageHandler
{
    public void Handle(ChatClient sender, Server server, byte[] data, int length);
}
