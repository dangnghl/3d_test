using Godot;
using System.Collections.Generic;


public partial class GameplayLogic : Node2D
{

    [Export] public InputModeResource InputUpData, InputDownData,InputPressData,InputReleaseData;
    [Export] public Node BlockGroup; // ToSpawn
    [Export] public PackedScene BlockPackedScene;

    private double _elapseTimer;
    private bool _pressed;
    private bool _needReset;
    private InputModeResource currClick;

    public override void _Ready()
    {
        SpawnBlock("vvvvv");
        SpawnBlock("==vvxxvx-=--");
    }
    public override void _Process(double delta)
    {
        
    }


    public void SpawnBlock(string code = "")
    {
        var insta = BlockPackedScene.Instantiate<Block>();
        BlockGroup.AddChild(insta);

        var l = new List<InputModeResource>();
        for (int i = 0; i < code.Length; i++)
        {
            l.Add(code2symbol(code[i]));
        }


        insta.EnqeueuSymbols(l.ToArray());
    }

    private InputModeResource code2symbol(char code)
    {
        return code switch
        {
            'v' => InputPressData,
            'x' => InputReleaseData,
            '-' => InputDownData,
            '=' => InputUpData,
            _ => throw new System.NotImplementedException(),
        };
    }
}
