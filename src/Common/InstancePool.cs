using System;
using System.Collections.Generic;
using System.Linq;

public class InstancePool<T>
{
    private readonly HashSet<T> _freeInstances = new();
    private readonly HashSet<T> _allInstances = new();
    private readonly Func<T> _instanceFactory;

    public InstancePool(Func<T> instanceFactory)
    {
        _instanceFactory = instanceFactory;
    }

    public InstancePool(IReadOnlyCollection<T> initialInstances, Func<T> instanceFactory) : this(instanceFactory)
    {
        if (initialInstances.Any())
        {
            _allInstances = initialInstances.ToHashSet();
            _freeInstances = initialInstances.ToHashSet();
        }
    }

    public T GetInstance()
    {
        T instance;

        if (_freeInstances.Any())
        {
            instance = _freeInstances.First();
            _freeInstances.Remove(instance);

            return instance;
        }

        instance = _instanceFactory.Invoke();
        _allInstances.Add(instance);

        return instance;
    }

    public void FreeInstance(T instance, Action<T> freeCallback = null)
    {
        if (!_allInstances.Contains(instance))
        {
            return;
        }

        freeCallback?.Invoke(instance);
        _freeInstances.Add(instance);
    }

    public void FreeAllInstances(Action<T> freeCallback = null)
    {
        foreach (var instance in _allInstances.Except(_freeInstances))
        {
            FreeInstance(instance, freeCallback);
        }
    }
}
