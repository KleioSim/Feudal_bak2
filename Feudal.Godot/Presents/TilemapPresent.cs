using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

internal partial class TilemapPresent : Present<TilemapView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var dictTileSource = new Dictionary<string, int>();
        
        var count = view.Tilemap.TileSet.GetSourceCount();
        for (int i = 0; i < count; i++)
        {
            var sourceId = view.Tilemap.TileSet.GetSourceId(i);
            var source = view.Tilemap.TileSet.GetSource(sourceId);

            dictTileSource.Add(source.ResourceName, sourceId);
        }

        view.Tilemap.Clear();

        foreach (var terrain in model.Terrains)
        {
            view.Tilemap.SetCell(0, new Vector2I(terrain.Position.x, terrain.Position.y), dictTileSource[terrain.TerrainType.ToString()], new Vector2I(0,0), 0);
        }
    }
}
