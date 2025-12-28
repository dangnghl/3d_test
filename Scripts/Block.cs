using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

public partial class Block : PanelContainer
{
    private readonly PackedScene SymbolScene = GD.Load<PackedScene>("res://Prefabs/symbol.tscn");
    public readonly static List<Block> BlockRegister = [];
    public Queue<InputModeResource> Pattern => _pattern;
    private Queue<InputModeResource> _pattern = new ();
    private Queue<Node> SymbolList = new();
    [Export] public string Code {get;set;}
    [Export] public Node PatternGroup;
    

    public void EnqeueuSymbols (params InputModeResource[] clickMode)
    {
        foreach (var symbol in clickMode)
        {
            _pattern.Enqueue(symbol);
        }
        RenderSymbols();
    }
    public override void _Ready()
    {
        RenderSymbols();
    }
    public override void _Process(double delta)
    {
    }

    public override void _EnterTree()
    {
        BlockRegister.Add(this);
    }

    public override void _ExitTree()
    {
        BlockRegister.Remove(this);
    }

    public void RenderSymbols()
    {
        foreach (InputModeResource click in _pattern)
        {
            var isnta = SymbolScene.Instantiate();
            PatternGroup.AddChild(isnta);
            isnta.GetNode<TextureRect>("Texture").Texture = click.Texture;
            SymbolList.Enqueue(isnta);
        }
    }

    internal void DequeueSymbol()
    {
        Pattern.Dequeue();
        if(SymbolList.TryDequeue(out var symbol))
        {
            symbol.QueueFree();
        }

    }

}
