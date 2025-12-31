using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InputManager
{

    private bool _prevPressed = false;
    private static IEnumerable<InputPair> TrimActiveInputPair(IEnumerable<InputPair> inputPairs)
    {
        if (!inputPairs.Any()) return [];

        return (inputPairs.Last().UpDuration == 0 || inputPairs.Last().DownDuration == 0) ?
        inputPairs.SkipLast(1): inputPairs;
    }
    
    private void GatherInput()
    {
        var inputChangeThisFrame = NowPressed != _prevPressed;
        if (inputChangeThisFrame)
        {
            if (NowPressed)
            {
                _inputPairBuffer[^1].DownDuration = 0;
            }
            else
            {
                _inputPairBuffer.Add(new());
            }
            _prevPressed = NowPressed;
        }
    }
    
}