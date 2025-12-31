using Godot;
using System;

public partial class Spawner : Node
{
    public event Action<Node> OnSpawned ;

    [Export] private PackedScene _spawnScene;
    [Export] private NodePath _spawnParentPath;
    [Export] private float _spawnInterval = 1.5f;
    [Export] private bool _autoStart = true;
    private Node _spawnParent;

    public override void _Ready()
    {
        _spawnParent = GetNodeOrNull(_spawnParentPath) ?? GetParent();
    }



    public Node Spawn()
    {
        if (_spawnScene == null)
        {
            GD.PushWarning("Spawner: No scene assigned.");
            return null;
        }

        var instance = _spawnScene.Instantiate();
        _spawnParent.AddChild(instance);

        OnSpawned?.Invoke(instance);
        return instance;
    }
}


