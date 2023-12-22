using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
internal partial class TilemapPresent : Present<TilemapView, ISession>
{
    protected override ISession MockModel
    {
        get
        {
            var mock = new SessionMock();
            mock.TerrainMocks.Add(new TerrainMock() { Position = (0, 0) });

            return mock;
        }
    }

    private string terrainType = nameof(Interfaces.TerrainType.Plain);
    public string TerrainType
    {
        get => terrainType;
        set
        {
            if (terrainType == value)
            {
                return;
            }

            terrainType = value;

            var list = model.Terrains as List<TerrainMock>;
            foreach (var terrain in list)
            {
                terrain.TerrainType = Enum.Parse<Interfaces.TerrainType>(terrainType);
            }

            isDirty = true;
        }
    }

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        properties.Add(new Dictionary()
        {
            { "name", nameof(TerrainType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", Enum.GetNames<Interfaces.TerrainType>()) }
        });

        return properties;
    }

    private bool isDiscovered = true;
    [Export]
    public bool IsDiscovered
    {
        get
        {
            return isDiscovered;
        }
        set
        {
            isDiscovered = value;

            var list = model.Terrains as List<TerrainMock>;
            foreach (var terrain in list)
            {
                terrain.IsDiscoverd = isDiscovered;
            }

            isDirty = true;
        }
    }

    [Export]
    public Vector2[] Positions
    {
        get
        {
            return model.Terrains.Select(terrain => new Vector2(terrain.Position.x, terrain.Position.y)).ToArray();
        }
        set
        {
            var list = model.Terrains as List<TerrainMock>;

            if (value.Length < list.Count)
            {
                list.RemoveRange(0, list.Count - value.Length);
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (list.Count <= i)
                {
                    list.Add(new TerrainMock());
                }

                list[i].Position = ((int)value[i].X, (int)value[i].Y);
                list[i].TerrainType = Enum.Parse<Interfaces.TerrainType>(terrainType);
                list[i].IsDiscoverd = isDiscovered;
            }

            isDirty = true;
        }
    }
}

public class TerrainMock : ITerrain
{
    public (int x, int y) Position { get; set; }

    public TerrainType TerrainType { get; set; }

    public bool IsDiscoverd { get; set; } = true;

    public IEnumerable<Interfaces.Resource> Resources { get; set; }

    public List<Interfaces.Resource> ResourceList { get; } = new List<Interfaces.Resource>();

    public TerrainMock()
    {
        Resources = ResourceList;
    }
}
