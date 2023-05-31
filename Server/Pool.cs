using System;

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

    public bool TryGetFree(out T? result)
    {
        for (int i = 0; i < _elements.Length; ++i)
        {
            var element = _elements[i];
            lock (element)
            {
                if (!element.IsSet())
                {
                    element.Set(i);
                    result = element;
                    return true;
                }
            }
        }
        result = null;
        return false;
    }

    public T[] GetActive()
    {
        return Array.FindAll(_elements, e =>
        {
            lock (e) return e.IsSet();
        });
    }
}
