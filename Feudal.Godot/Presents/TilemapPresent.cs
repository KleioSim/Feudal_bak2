using Feudal.Interfaces;
using Godot;

namespace Feudal.Godot.Presents;

internal partial class TilemapPresent : Present<TilemapView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var count = view.Tilemap.TileSet.GetSourceCount();
        GD.Print(count);

        for (int i = 0; i < count; i++)
        {
            GD.Print(view.Tilemap.TileSet.GetSourceId(i));
        }

        view.Tilemap.Clear();

        foreach (var terrain in model.Terrains)
        {
            view.Tilemap.SetCell(0, new Vector2I(terrain.Position.x, terrain.Position.y), 0, new Vector2I(0,0), 0);
        }
    }
}
