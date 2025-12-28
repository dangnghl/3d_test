using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InputManager : Node2D
{
    public struct MatchInputPair(bool HoldUp,bool HoldDown, double UpDuration, double DownDuration)
    {
        public bool NeedHoldUp{ get; set; } = HoldUp;
        public bool NeedHoldDown{ get; set; } = HoldDown;
        public double UpDuration { get; set; } = UpDuration;
        public double DownDuration { get; set;} = DownDuration;

        public bool IsMatch(InputPair inputPair)
        {
            return (NeedHoldUp ? inputPair.UpDuration >= this.UpDuration : inputPair.UpDuration != 0) &&
            (NeedHoldDown ? inputPair.DownDuration >= this.DownDuration : inputPair.DownDuration != 0);

        }
    }
    public record InputPair(double UpDuration = 0, double DownDuration = 0)
    {
        public double UpDuration { get; set; } = UpDuration;
        public double DownDuration { get; set;} = DownDuration;
        public override string ToString()
        {
            return $"[(Up,{UpDuration}),(Down,{DownDuration})]";
        }

        public void OnActive(bool selectDown,double delta)
        {
            if(selectDown)
                DownDuration += delta;
            else
                UpDuration += delta;
        }
    }
}