using Godot;
using System.Diagnostics;
using System.Linq;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public partial class GameplayLogic : Node2D
{

    [Export] private MatchInputPair _inputPressData, _inputUpData, _inputDownData;
    [Export] private BlockGroup        _blockGroup;
    [Export] private InputManager   _inputManager;

    public override void _Ready()
    {
        Debug.Assert(_inputPressData != null);
        Debug.Assert(_inputUpData != null);
        Debug.Assert(_inputDownData != null);
        Debug.Assert(_blockGroup != null);
        
        var n = GetNode<BlockGroup>("BlockGroup");

        //SpawnBlock("vvvvv");
        SpawnBlock("-vv=v-");

    }
    public override void _Input(InputEvent @event)
    {
        var _inputPairBuffer = _inputManager.GetInputPairs();
        if (Input.IsKeyPressed(Key.D))
        {
            string a = "";
            foreach (var inputBehavior in _inputPairBuffer)
                a += (inputBehavior.ToString());
            GD.Print(a);
        }

        if (Input.IsKeyPressed(Key.S))
        {
            var block = _blockGroup.GetBlock(0);

            var inputPattern = _inputManager.GetInputTrimmedPairs().ToArray();
            var matchingPattern = block.GetPattern().ToArray();

            int[] v = WindowSlideSearch.FindIndices(
                inputPattern, matchingPattern,
                (i,p) => p.IsMatch(i),0
                );
            
            if(v.Length != 0)  _blockGroup.RemoveSymbolsRange(block,v[0],1);
            //GD.Print(matchingPattern);
            

        }
        if (Input.IsKeyPressed(Key.A))
        {
            _inputManager.ResetBuffer();
        }
    }
    public void SpawnBlock(string code = "")
    {
        var blockObj = _blockGroup.Spawner.Spawn() as Block;    
        blockObj.InsertSymbols(String2Symbols(code));
    }

    private MatchInputPair[] String2Symbols(string code)
    {
        var inputs = new MatchInputPair[code.Length];
        for (int i = 0; i < code.Length; i++)
        {
            var pair = Code2Symbol(code[i]);
            inputs.SetValue(pair,i);
        }
        return inputs;
    }

    private MatchInputPair Code2Symbol(char code)
    {
        return code switch
        {
#if false
            'v' => new MatchInputPair(false, false, 0.0, 0.0),
            '-' => new MatchInputPair(false, true, 0.0, 2.0),
            '=' => new MatchInputPair(true, false, 2.0, 0.0),
            _ => throw new System.NotImplementedException(),
#else
            'v' => _inputPressData,
            '-' => _inputDownData,
            '=' => _inputUpData,
            _ => throw new System.NotImplementedException(),
#endif
        };
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
