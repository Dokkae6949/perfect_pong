using System;
using System.Collections.Generic;

namespace test.lib.auto_prop;

public class AutoProp<T> : IAutoProp<T>
{
    public event Action<T>? Changed;
    public event Action<T>? Synced
    {
        add
        {
            InternalSynced += value;
            value?.Invoke(Value);
        }
        remove => InternalSynced -= value;
    }
    public event Action? Completed;
    public event Action<Exception>? Errored;
    
    private event Action<T>? InternalSynced;
    
    
    public T Value { get; private set; }
    public IEqualityComparer<T> Comparer { get; }
    
    
    private bool _completed;
    private bool _busy;
    private readonly Queue<T> _pendingChanges = new();
    

    /// <summary>
    /// Create a new AutoProp with the given value.
    /// </summary>
    /// <param name="value"></param>
    public AutoProp(T value)
    {
        Value = value;
        Comparer = EqualityComparer<T>.Default;
    }
    
    /// <summary>
    /// Create a new AutoProp with the given value and comparer.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="comparer"></param>
    public AutoProp(T value, IEqualityComparer<T> comparer)
    {
        Value = value;
        Comparer = comparer;
    }


    /// <summary>
    /// Pushes a new value to the property. If the value is different
    /// from the current value, the property will be updated and the
    /// <see cref="Changed"/> and <see cref="Synced"/> event handlers
    /// will be invoked.
    /// </summary>
    /// <param name="value">New value</param>
    public void OnChanged(T value)
    {
        if (_completed) return;

        _pendingChanges.Enqueue(value);
        
        if (_busy) return;

        lock (_pendingChanges)
        {
            _busy = true;

            while (_pendingChanges.Count > 0 && !_completed)
            {
                var nextValue = _pendingChanges.Dequeue();
                
                if (Comparer.Equals(Value, nextValue)) continue;
                
                Value = nextValue;
                Changed?.Invoke(nextValue);
                InternalSynced?.Invoke(nextValue);
            }
            
            _busy = false;
        }
    }
    
    /// <summary>
    /// Pushes a new value to the property. If the value is different
    /// from the current value, the property will be updated and the
    /// <see cref="Changed"/> and <see cref="Synced"/> event handlers
    /// will be invoked.
    /// </summary>
    /// <param name="value">Function, providing the current value, returning the new value</param>
    public void OnChanged(Func<T, T> value)
    {
        OnChanged(value(Value));
    }
    
    /// <summary>
    /// Marks the property as completed. This will prevent
    /// any further changes from being pushed to the property.
    /// </summary>
    public void OnCompleted()
    {
        if (_completed) return;
        
        _completed = true;
        _pendingChanges.Clear();
        
        Completed?.Invoke();
    }
    
    /// <summary>
    /// Pushes an exception to the property and invokes the <see cref="Errored"/>
    /// event handler. Errors can be pushed to the property more than once and
    /// do not throw the exceptions. Pushing an exception will not prevent
    /// further changes from being pushed to the property.
    /// </summary>
    /// <param name="exception">Exception to push</param>
    public void OnErrored(Exception exception)
    {
        if (_completed) return;
        
        Errored?.Invoke(exception);
    }
    
    /// <summary>
    /// Clears all event handlers from the property.
    /// </summary>
    public void Clear()
    {
        Changed = null;
        InternalSynced = null;
        Completed = null;
        Errored = null;
    }
}