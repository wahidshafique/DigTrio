using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Behaves as a generic stack with random access
 * -- always check size to be safe --
 */
public class StackList<T> {
    List<T> list;
    
    // Default constructor
    public StackList()
    {
        list = new List<T>();
    }

    // Set the initial capacity with this constructor
    public StackList(int capacity)
    {
        list = new List<T>(capacity);
    }
    
    // Push item ontop of list
    public void Push(T obj)
    {
        list.Insert(list.Count, obj);
    }

    // Pop top item of list
    public void Pop()
    {
        if (list.Count > 0)
            list.RemoveAt(list.Count - 1);
    }

    // Peek at the top item
    public T Peek()
    {
        return list[list.Count - 1];
    }

    // Gets the appropriate element from them list
    // Returns element 0 if element is out of range
    public T GetItem(int i)
    {
        if (i > (list.Count - 1) || i < 0)
        {
            i = 0;
        }

        return list[i];
    }

    // Get the size of the list
    public int GetSize()
    {
        return list.Count;
    }

    // Copy to an array
    public void CopyTo(T[] container)
    {
        list.CopyTo(container);
    }

    public int Count
    {
        get
        {
            return list.Count;
        }
    }
}
