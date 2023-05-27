using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class ConcurrentHashSet<T>
: ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
where T : notnull
{
    private readonly ConcurrentDictionary<T, byte> _dictionary = new();
    public int Count => _dictionary.Keys.Count;
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _dictionary.TryAdd(item, 0);
    }
    public void Clear()
    {
        _dictionary.Clear();
    }
    public bool Contains(T item)
    {
        return _dictionary.ContainsKey(item);
    }
    public void CopyTo(T[] array, int arrayIndex)
    {
        _dictionary.Keys.CopyTo(array, arrayIndex);
    }
    public IEnumerator<T> GetEnumerator()
    {
        return _dictionary.Keys.GetEnumerator();
    }
    public bool Remove(T item)
    {
        return _dictionary.TryRemove(item, out byte b);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.Keys.GetEnumerator();
    }
}
