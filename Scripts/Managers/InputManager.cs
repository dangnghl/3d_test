using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class InputManager : Node
{
    public static InputManager      Instance { get; private set; }
    private static bool             NowPressed => Input.IsKeyPressed(Key.Space);

    public bool                     IsRecordingInput { get; set; } = true;
    
    private readonly List<InputPair> _inputPairBuffer = [];

    public override void _Ready()
    {
        this.ResetBuffer();
    }
    
    public override void _Input(InputEvent @event)
    {
        this.GatherInput();
    }

    public override void _Process(double delta)
    {
        if(_inputPairBuffer.Count != 0)
            _inputPairBuffer[^1].OnActive(NowPressed, delta);
    }

    public void ResetBuffer()
    {
        _inputPairBuffer.Clear();
        _inputPairBuffer.Add(new());
    }
    public IEnumerable<InputPair> GetInputPairs() => _inputPairBuffer;
    public IEnumerable<InputPair> GetInputTrimmedPairs() => TrimActiveInputPair(_inputPairBuffer);
    
    


}