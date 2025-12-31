using Godot;
using System;

using System.Collections.Generic;
using System.Linq;

public partial class ObjectRegistry : Node
{
    private readonly List<Node> _alive = [];

    public event Action<Node> OnRegistered;
    public event Action<Node> OnUnRegistered;

    public void Register(Node node,int index)
    {
        if (node == null || !_alive.Contains(node))
            return;
        
        _alive.Insert(index,node);

        OnRegistered?.Invoke(node);
        node.TreeExiting += () => Unregister(node);
    }

    public void Register(Node node)
    {
        if (node == null || _alive.Contains(node))
            return;
        
        _alive.Add(node);

        OnRegistered?.Invoke(node);
        node.TreeExiting += () => Unregister(node);
    }
    public void RegisterRange(Node[] nodes)
    {
        if (nodes == null || nodes.Length == 0 || !_alive.Any((obj)=> nodes.Contains(obj)))
            return;
        
        _alive.AddRange(nodes);

        foreach (Node node in nodes)
        {
            OnRegistered?.Invoke(node);
            node.TreeExiting += () => Unregister(node);
        }
    }

    public void Unregister(Node node)
    {
        if (_alive.Remove(node))
            OnUnRegistered?.Invoke(node);
    }

    public void UnregisterRange(int start, int count)
    {
        for (int i = start; i < start + count; i++)
            OnUnRegistered?.Invoke(_alive[i]);
        _alive.RemoveRange(start,count);
    }



    // Generic accessors (this is where T lives)
    public IEnumerable<T> GetAll<T>() where T : Node
    {
        foreach (var node in _alive)
            if (node is T t)
                yield return t;
    }

    public void ForEach<T>(Action<T> action) where T : Node
    {
        foreach (var node in _alive)
            if (node is T t)
                action(t);
    }
}