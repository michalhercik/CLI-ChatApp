using System.Collections.Generic;

namespace ChatAppClient;

public class RecieverCollection
{
    private List<IReciever> _idModules = new();
    private Dictionary<string, IReciever> _nameModules = new();

    public RecieverCollection() { }

    public void Add(IReciever module)
    {
        module.Id = (short)_idModules.Count;
        _idModules.Add(module);
        _nameModules.Add(module.Name, module);
    }

    public IReciever Get(int id)
        => _idModules[id];

    public bool TryGet(string name, out IReciever module)
    {
        if (_nameModules.ContainsKey(name))
        {
            _nameModules.TryGetValue(name, out module!);
            return true;
        }
        module = Get(0);
        return false;
    }

    public bool TryGet(int id, out IReciever module)
    {
        if (0 <= id && id < _idModules.Count)
        {
            module = _idModules[id];
            return true;
        }
        module = Get(0);
        return false;
    }
}

