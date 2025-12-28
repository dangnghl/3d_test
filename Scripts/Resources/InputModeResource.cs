using Godot;


[GlobalClass]
public partial class InputModeResource : Resource
{
    [Export] public Texture2D Texture;
    [Export] public bool NeedReset;
    [Export] public bool ShouldInputDown;
    [Export] public bool IsProgress;
    [Export] public float Duration = 0;
}
