using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Simple helper class that provides instance pooling functionality.
/// </summary>
/// <typeparam name="T"></typeparam>
public class InstancePool<T>
{
    private readonly HashSet<T> _freeInstances = new();
    private readonly HashSet<T> _allInstances = new();
    private readonly Func<T> _instanceFactory;

    public int Count => _allInstances.Count;
    public int CountFree => _freeInstances.Count;
    public int CountBusy => _allInstances.Count(i => !_freeInstances.Contains(i));

    /// <summary>
    /// Base constructor of the InstancePool, doesn't perform any instance allocation.
    /// </summary>
    /// <param name="instanceFactory">Func<T> delegate that will be used to acquire new instances</param>
    public InstancePool(Func<T> instanceFactory)
    {
        _instanceFactory = instanceFactory;
    }

    /// <summary>
    /// Constructor that supports providing initial instances to InstancePool.
    /// </summary>
    /// <param name="initialInstances">initial instances, all will be considered free unless GetInstance is called and they're marked as busy</param>
    /// <param name="instanceFactory">Func<T> delegate that will be used to acquire new instances</param>
    public InstancePool(IReadOnlyCollection<T> initialInstances, Func<T> instanceFactory) : this(instanceFactory)
    {
        if (initialInstances.Any())
        {
            // Calling ToHashSet() here individually to create two independent copies 
            _allInstances = initialInstances.ToHashSet();
            _freeInstances = initialInstances.ToHashSet();
        }
    }

    /// <summary>
    /// This method will return a free instance or allocate a new one.
    /// </summary>
    /// <returns>T - object of instance type</returns>
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

    /// <summary>
    /// This method will free instance if it exists in the InstancePool. If instance doesn't exist nothing happens.
    /// </summary>
    /// <param name="instance">instance to free</param>
    /// <param name="freeCallback">optional callback to execute on the instance before it is freed</param>
    public void FreeInstance(T instance, Action<T> freeCallback = null)
    {
        if (!_allInstances.Contains(instance))
        {
            return;
        }

        freeCallback?.Invoke(instance);
        _freeInstances.Add(instance);
    }

    /// <summary>
    /// This method will free instance all of the instances. If no instances exist nothing happens.
    /// </summary>
    /// <param name="freeCallback">optional callback to execute on the instance before it is freed</param>
    public void FreeAllInstances(Action<T> freeCallback = null)
    {
        foreach (var instance in _allInstances.Except(_freeInstances))
        {
            FreeInstance(instance, freeCallback);
        }
    }

    // TODO:
    // Currently methods below are not used anywhere to avoid extra overhead.  
    // Realistically, this is only used in Skills for now so we could end-up with ~20 instances allocated in the worst case scenario.
    // Most actor I saw have 1-3 skills at best and therefore initial 5 instances allocated in skills should be enough.
    // I'm leaving them here in case we might want to implement system that will destroy instances that are unused in future.

    /// <summary>
    /// Method that will shrink the InstancePool to the size of instances that are currently busy. 
    /// </summary>
    /// <param name="shrinkCallback">Callback to execute on the instance before it's removed</param>
    public void ShrinkToOnlyBusy(Action<T> shrinkCallback = null)
    {
        if (_freeInstances.Count == 0)
        {
            return;
        }

        foreach (var instance in _freeInstances)
        {
            _allInstances.Remove(instance);
            shrinkCallback?.Invoke(instance);
        }

        _freeInstances.Clear();
    }

    /// <summary>
    /// Method that shrinks amount of free instances in the InstancePool to minimum count.
    /// </summary>
    /// <param name="minimumCount">minimum count of free instances, if it is equal to or bigger than the current number of free instances method will exit</param>
    /// <param name="shrinkCallback">Callback to execute on the instance before it's removed</param>
    public void ShrinkFree(int minimumCount, Action<T> shrinkCallback = null)
    {
        if (_freeInstances.Count <= minimumCount)
        {
            return;
        }

        var instancesToFree = _freeInstances.Skip(minimumCount).ToList();

        foreach (var instance in instancesToFree)
        {
            _freeInstances.Remove(instance);
            _allInstances.Remove(instance);
            shrinkCallback?.Invoke(instance);
        }
    }
}
