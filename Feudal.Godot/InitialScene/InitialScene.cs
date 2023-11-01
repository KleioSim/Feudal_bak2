using Feudal.Sessions.Builders;
using Godot;
using System;

public partial class InitialScene : Node2D
{
    public override void _Ready()
    {
        base._Ready();

        PresentManager.model = SessionBuilder.Build();

        GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
    }
}
