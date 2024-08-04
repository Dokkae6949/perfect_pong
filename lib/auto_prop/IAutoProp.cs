using System;
using System.Collections.Generic;

namespace test.lib.auto_prop;

public interface IAutoProp<T>
{
    /// <summary>
    /// Changed handler that is called when the value is updated with
    /// a new value that is different from the previous value.
    /// </summary>
    event Action<T>? Changed;
    /// <summary>
    /// Synced handler that is called as soon as it is added, as well
    /// as when the value is updated with a new value that is different
    /// from the previous value.
    /// </summary>
    event Action<T>? Synced;
    /// <summary>
    /// Completed handler that is called when the value is marked as completed.
    /// This means no new values will be accepted.
    /// </summary>
    event Action? Completed;
    /// <summary>
    /// Errored handler that is called when an exception is thrown.
    /// </summary>
    event Action<Exception>? Errored;
    
    /// <summary>
    /// Current value of the property.
    /// This is always the most recent value.
    /// </summary>
    T Value { get; }
    /// <summary>
    /// The comparer used to determine if the value has changed.
    /// </summary>
    IEqualityComparer<T> Comparer { get; }
}