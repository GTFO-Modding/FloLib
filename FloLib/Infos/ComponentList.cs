using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Infos;
/// <summary>
/// Component List used for <see cref="LG_Objects"/>
/// </summary>
/// <typeparam name="T">Component Type</typeparam>
public sealed class ComponentList<T> : IEnumerable<T> where T : Component
{
    private readonly List<T> _InternalList = new();
    private T[] _ArrayCache = Array.Empty<T>();

    /// <summary>
    /// Get Array of Items
    /// </summary>
    public T[] Items => _ArrayCache;

    internal void Add(T itemToAdd)
    {
        var id = itemToAdd.GetInstanceID();
        if (!_InternalList.Any(t => t.GetInstanceID() == id))
        {
            _InternalList.Add(itemToAdd);
            _ArrayCache = _InternalList.ToArray();
        }
    }

    internal void AddRange(IEnumerable<T> itemsToAdd)
    {
        foreach (var item in itemsToAdd)
        {
            Add(item);
        }
    }

    internal void Remove(T itemToRemove)
    {
        var id = itemToRemove.GetInstanceID();
        var index = _InternalList.FindIndex(i => i.GetInstanceID() == id);
        if (index > -1)
        {
            _InternalList.RemoveAt(index);
            _ArrayCache = _InternalList.ToArray();
        }
    }

    internal void Clear()
    {
        _InternalList.Clear();
        _ArrayCache = Array.Empty<T>();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _InternalList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _InternalList.GetEnumerator();
    }
}
