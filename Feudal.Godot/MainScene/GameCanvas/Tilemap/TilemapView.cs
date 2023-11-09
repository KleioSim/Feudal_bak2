using Godot;
using System;

public partial class TilemapView : ViewControl
{
    public TileMap Tilemap => GetNode<TileMap>("TileMap");
    public Camera2D Camera => GetNode<Camera2D>("Camera2D");

    public override void _Ready()
    {
        base._Ready();
        Camera.Position = Tilemap.MapToLocal(new Vector2I(0, 0));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventKey)
        {
            if (eventKey.Pressed)
            {
                if (eventKey.ButtonIndex == MouseButton.Left)
                {
                    GD.Print(Tilemap.LocalToMap(GetGlobalMousePosition()));
                }
                else if (eventKey.ButtonIndex == MouseButton.WheelDown)
                {
                    Camera.Zoom += new Vector2(0.1f, 0.1f);
                }
                else if (eventKey.ButtonIndex == MouseButton.WheelUp)
                {
                    Camera.Zoom -= new Vector2(0.1f, 0.1f);
                }
            }
            return;
        }
    }

    public void OnScreenMoved(float angle)
    {
        GD.Print($"OnCanvasMoved {angle}");

        //GD.Print($"MapControl.Position {MapControl.Position}");

        Camera.Position = Camera.Position + (Vector2.Right * 10).Rotated(angle);

        //MapControl.Position.MoveToward
    }
}
