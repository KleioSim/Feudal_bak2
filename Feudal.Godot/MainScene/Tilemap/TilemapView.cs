using Godot;
using System;

public partial class TilemapView : ViewControl
{
    public TileMap Tilemap => GetNode<TileMap>("CenterContainer/TileMap");

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventKey)
        {
            if(eventKey.Pressed)
            {
               GD.Print(Tilemap.LocalToMap(Tilemap.ToLocal(eventKey.Position)));
            }
        }
    }
}
