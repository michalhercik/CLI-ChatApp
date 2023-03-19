namespace ChatApp;

public class Pool<T> where T : class, IPoolElement, new()
{
    private T[] _elements;

    public Pool(int size)
    {
        _elements = new T[size];

        for (int i = 0; i < _elements.Length; ++i)
        {
            _elements[i] = new T();
        }
    }

    public T GetFree()
    {
        for (int i = 0; i < _elements.Length; ++i) 
        {
            var element = _elements[i];
            if (!element.IsSet())
            {
                element.Set(i);
                return element;
            }
        }
        // TODO: handle no free client
        return null;
    }
}
