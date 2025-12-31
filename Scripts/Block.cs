using Godot;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;

public partial class Block : PanelContainer
{
    private readonly PackedScene SymbolScene = GD.Load<PackedScene>("res://Prefabs/symbol.tscn");
    public readonly static List<Block> BlockRegister = [];
    List<MatchInputPair> _matchingPairs = new();
    [Export] public string Code {get;set;}
    [Export] public Node PatternGroup;
    [Export] public ObjectRegistry SymbolRegistry {get;private set;}
    
    public int SymbolCount => _matchingPairs.Count;

    public override void _Ready()
    {
        SymbolRegistry.OnUnRegistered += (obj) =>
        {
            obj.QueueFree();
        };

        SymbolRegistry.OnRegistered += (obj) =>
        {
            PatternGroup.AddChild(obj);
        };
    }


    public IEnumerable<MatchInputPair> GetPattern()
    {
        foreach (var node in _matchingPairs)
            if (node is MatchInputPair t)
                yield return t;
    }
    public void InsertSymbols (params MatchInputPair[] pattern)
    {
        
        foreach (MatchInputPair symbol in pattern)
        {
            _matchingPairs.Add(symbol); // register into memory

            var obj = SymbolScene.Instantiate();
            obj.GetNode<TextureRect>("Texture").Texture = symbol.Texture;

            SymbolRegistry.Register(obj);
            
        }
    }
    public void RemoveSymbolRange(int index,int count)
    {
        SymbolRegistry.UnregisterRange(index,count);
        _matchingPairs.RemoveRange(index,count); // Remove from regisiters
    }

    public int[] FindOverlappedPattern(InputPair[] findPattern,int limit = 0)
    {
        return WindowSlideSearch.FindIndices(findPattern, _matchingPairs.ToArray(), (i,p) => p.IsMatch(i),limit);
    }
}
