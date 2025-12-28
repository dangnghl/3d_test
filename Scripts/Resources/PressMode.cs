using Godot;


[GlobalClass]
public partial class ClickResource : Resource
{
    [Export] public Texture2D Texture;
    [Export] public bool NeedReset;
    [Export] public bool IsInputDown;
    [Export] public float Duration = 0;
}
