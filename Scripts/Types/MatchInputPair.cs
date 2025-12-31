using System.Diagnostics;
using Godot;

[GlobalClass]
public partial class MatchInputPair : Resource
{
    [Export] public Texture2D Texture;

    public MatchInputPair()
    {
        
    }
    public MatchInputPair(
        bool HoldUp = false,
        bool HoldDown = false,
        double UpDuration = 0.0,
        double DownDuration = 0.0)
    {
        NeedHoldUp = HoldUp;
        NeedHoldDown = HoldDown;
        this.UpDuration = UpDuration;
        this.DownDuration = DownDuration;
    }


    [Export] public bool NeedHoldUp;
    [Export] public bool NeedHoldDown;
    [Export] public double UpDuration;
    [Export] public double DownDuration;

    public bool IsMatch(InputPair inputPair)
    {
        return (NeedHoldUp ? inputPair.UpDuration >= this.UpDuration : inputPair.UpDuration != 0) &&
        (NeedHoldDown ? inputPair.DownDuration >= this.DownDuration : inputPair.DownDuration != 0);
    }
    public override string ToString()
    {
        return $"[(Up:{NeedHoldUp},{UpDuration}),(Down:{NeedHoldDown},{DownDuration})]";
    }

}
