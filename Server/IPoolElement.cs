namespace ChatApp;

public interface IPoolElement
{
    public bool IsSet();
    public void Set(int id);
    public void Unset();
}
