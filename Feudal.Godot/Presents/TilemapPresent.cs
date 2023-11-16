using Feudal.Interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

internal partial class TilemapPresent : Present<TilemapView, ISession>
{
    private Dictionary<string, int> dictTileSetDiscoverd;
    private Dictionary<string, int> dictTileSetNotDiscoverd;
    private const string notDiscoverdFlag = "_Not_Discovered";

    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        view.Tilemap.Clear();

        foreach (var terrain in model.Terrains)
        {
            view.Tilemap.SetCell(0, new Vector2I(terrain.Position.x, terrain.Position.y), GetTileSetId(terrain.TerrainType.ToString(), terrain.IsDiscoverd), new Vector2I(0, 0), 0);
        }
    }

    private int GetTileSetId(string name, bool isDiscoverd)
    {
        dictTileSetDiscoverd ??= BuildDictDiscoverd();
        dictTileSetNotDiscoverd ??= BuildDictNotDiscoverd();

        var dict = isDiscoverd ? dictTileSetDiscoverd : dictTileSetNotDiscoverd;

        return dict[name];
    }

    private Dictionary<string, int> BuildDictNotDiscoverd()
    {
        var dictTileSource = new Dictionary<string, int>();

        var count = view.Tilemap.TileSet.GetSourceCount();
        for (int i = 0; i < count; i++)
        {
            var sourceId = view.Tilemap.TileSet.GetSourceId(i);
            var source = view.Tilemap.TileSet.GetSource(sourceId);

            if (source.ResourceName.EndsWith(notDiscoverdFlag))
            {
                dictTileSource.Add(TrimEnd(source.ResourceName, notDiscoverdFlag), sourceId);
            }
        }

        return dictTileSource;
    }

    private Dictionary<string, int> BuildDictDiscoverd()
    {
        var dictTileSource = new Dictionary<string, int>();

        var count = view.Tilemap.TileSet.GetSourceCount();
        for (int i = 0; i < count; i++)
        {
            var sourceId = view.Tilemap.TileSet.GetSourceId(i);
            var source = view.Tilemap.TileSet.GetSource(sourceId);

            if (!source.ResourceName.EndsWith(notDiscoverdFlag))
            {
                dictTileSource.Add(source.ResourceName, sourceId);
            }
        }

        return dictTileSource;
    }

    private static string TrimEnd(string source, string value)
    {
        if (!source.EndsWith(value))
            return source;

        return source.Remove(source.LastIndexOf(value));
    }
}
