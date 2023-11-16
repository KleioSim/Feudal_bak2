using Godot;
using System;

public partial class GameMainView : ViewControl
{
    public TilemapView Tilemap => GetNode<TilemapView>("Tilemap");

    public void OnCanvasMoved(float angle)
    {
        GD.Print($"OnCanvasMoved {angle}");

        //GD.Print($"MapControl.Position {MapControl.Position}");

        Tilemap.Camera.Position = Tilemap.Camera.Position + (Vector2.Right * 10).Rotated(angle);

        //MapControl.Position.MoveToward
    }


}
