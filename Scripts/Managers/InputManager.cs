using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InputManager : Node2D
{
    public static InputManager Instance { get; private set; }
    List<InputPair> _inputPairs = new();
    double _elapseTime;
    private bool _prevPressed = false;
    static bool NowPressed => Input.IsKeyPressed(Key.Space);

    
    public override void _Ready()
    {
        ResetBuffer();
    }
    
    public override void _Input(InputEvent @event)
    {
        GatherInput();
        

        if (Input.IsKeyPressed(Key.D))
        {
            string a = "";
        foreach (var inputBehavior in _inputPairs)
            a += (inputBehavior.ToString());
            GD.Print(a);
        }

        if (Input.IsKeyPressed(Key.S))
        {
            var trimmedInputPairs = TrimActiveInputPair(_inputPairs);

            var v = FindSlidingWindowMatchIndices<InputPair,MatchInputPair>(trimmedInputPairs.ToArray(), matchingPattern.ToArray(), (i,p) => p.IsMatch(i),0);
            if(v.Length != 0)  matchingPattern.RemoveRange(v[0],_inputPairs.Count);
            GD.Print(matchingPattern);
        }
        if (Input.IsKeyPressed(Key.A))
        {
            ResetBuffer();
        }
    }

    readonly List<MatchInputPair> matchingPattern = [
            new(false,false,0.5,0.5),
            new(false,true,0.5,2.0),
            new(false,false,0.5,0.5),
            ];

    public override void _Process(double delta)
    {
        if(_inputPairs.Count != 0)
            _inputPairs[^1].OnActive(NowPressed, delta);
    }

    


}