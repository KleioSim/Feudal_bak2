using Feudal.Sessions.Builders;
using Godot;
using System;

public partial class InitialScene : Node2D
{
    public override void _Process(double delta)
    {
        base._Process(delta);

        Present.model = SessionBuilder.Build();

        GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
    }
}
