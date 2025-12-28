using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InputManager : Node2D
{
    private static IEnumerable<InputPair> TrimActiveInputPair(IEnumerable<InputPair> inputPairs)
    {
        if (!inputPairs.Any()) return [];

        return (inputPairs.Last().UpDuration == 0 || inputPairs.Last().DownDuration == 0) ?
        inputPairs.SkipLast(1): inputPairs;
    }
    private static Span<int> FindSlidingWindowMatchIndices<T1,T2>(ReadOnlySpan<T1> window,ReadOnlySpan<T2> array,Func<T1,T2,bool> matchCondition,int count = 0)
    {
        if(window.IsEmpty) return [];
        if(window.Length > array.Length) return [];

        var slideLength = array.Length - window.Length + 1;
        var limitCounter = count > 0 ? Math.Min(count, slideLength) : slideLength ;

        List<int> matchesIndex = [];
        for (int i = 0; i < slideLength; i++)
        {
            //Window slide
            bool matched = true;
            for (int j = 0; j < window.Length; j++)
            {
                if(!matchCondition(window[j],array[i+j]))
                {
                    matched = false;
                    break;
                }
            }

            if (matched)
            {
                matchesIndex.Add(i);

                limitCounter--;
                if(limitCounter == 0)
                    break;
            }
        }
        return matchesIndex.ToArray();
    }
    private void GatherInput()
    {
        var inputChangeThisFrame = NowPressed != _prevPressed;
        if (inputChangeThisFrame)
        {
            if (NowPressed)
            {
                _inputPairs[^1].DownDuration = 0;
            }
            else
            {
                _inputPairs.Add(new());
            }
            _prevPressed = NowPressed;
        }
    }
    public void ResetBuffer()
    {
        _inputPairs.Clear();
        _inputPairs.Add(new());
    }
    public Span<int> FindOverlappedPattern(Span<MatchInputPair> pattern,int limit = 0)
    {
        var trimmedInputPairs = TrimActiveInputPair(_inputPairs).ToArray();
        return FindSlidingWindowMatchIndices<InputPair,MatchInputPair>(trimmedInputPairs, pattern, (i,p) => p.IsMatch(i),limit);
    }
}