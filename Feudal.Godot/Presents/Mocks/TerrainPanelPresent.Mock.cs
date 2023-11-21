using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Feudal.Godot.Presents;

internal partial class TerrainPanelPresent : Present<TerrainPanelView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new[]
        {
            new TerrainMock()
            {
                Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y),
                TerrainType = Interfaces.TerrainType.Plain,
                IsDiscoverd = false,
                WorkHoodId = WorkHoodPanelView.DefaultId
            }
        },
        WorkHoods = new List<IWorkHood>
        {
            new DiscoverWorkHood_Mock()
            {
                Id = WorkHoodPanelView.DefaultId
            }
        }
    };

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

            var terrain = model.Terrains.First() as TerrainMock;
            terrain.TerrainType = Enum.Parse<Interfaces.TerrainType>(terrainType);

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

            var terrain = model.Terrains.First() as TerrainMock;
            terrain.IsDiscoverd = isDiscovered;

            isDirty = true;
        }
    }
}

public class WorkHood_Mock : IWorkHood
{
    public string Id { get; set; }
    public ITask Task { get; set; }
}

public class DiscoverWorkHood_Mock : WorkHood_Mock, IDiscoverWorkHood
{
    public int DiscoverdPercent { get; set; }

    public (int x, int y) Position { get; set; }
}