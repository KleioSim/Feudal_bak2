using Godot;
using System;

public partial class MainSceneView : ViewControl
{
    public Control MapControl => GetNode<Control>("Map");

    public void OnCanvasMoved(float angle)
    {
        GD.Print($"OnCanvasMoved {angle}");

        GD.Print($"MapControl.Position {MapControl.Position}");

        MapControl.Position = MapControl.Position + (Vector2.Left * 10).Rotated(angle);

        //MapControl.Position.MoveToward
    }
}
