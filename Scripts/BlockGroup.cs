using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class BlockGroup : Node
{
    public Spawner Spawner { get; private set; }
    public ObjectRegistry Registry { get; private set; }

    public override void _Ready()
    {
        Spawner = GetNode<Spawner>("Spawner");
        Registry = GetNode<ObjectRegistry>("ObjectRegistry");
    
        Spawner.OnSpawned += (obj) =>
        {
            Registry.Register(obj);
        };

        Registry.OnUnRegistered += (obj) =>
        {
            obj.QueueFree();
        };


        foreach (Node node in GetChildren())
        {
            if(node is Block block)
                Registry.Register(block);
        }
    }

    public void RemoveSymbolsRange(Block block, int start, int count)
    {
        block.RemoveSymbolRange(start,count);

        if(block.SymbolCount == 0)
        {
            Registry.Unregister(block);
        }
    }

    public Block GetBlock(int index)
    {
        return Registry.GetAll<Block>().ElementAt(index);
    }



}
