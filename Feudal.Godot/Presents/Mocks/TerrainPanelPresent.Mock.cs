using Feudal.Interfaces;
using Feudal.Messages;
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
                Resources = new []{ Interfaces.Resource.FatSoild },
                IsDiscoverd = false,
            }
        },
        WorkHoods = new List<IWorkHood>
        {
            new DiscoverWorkHood_Mock()
            {
                Id = DiscoverWorkHoodPanelView.DefaultId,
                Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y),
            }
        },
        Tasks = new List<ITask>
        {

        },
        Clans = new List<IClan>()
        {
            new ClanMock()
            {
                Id = ClanPanelView.DefaultId
            }
        },
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

            SendUICommand(new MESSAGE_MockUpdate());
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

            SendUICommand(new MESSAGE_MockUpdate());
        }
    }
}

public class WorkHood_Mock : IWorkHood
{
    public string Id { get; set; }
    public ITask Task { get; set; }

    public void Run()
    {
        throw new NotImplementedException();
    }
}

public class DiscoverWorkHood_Mock : WorkHood_Mock, IDiscoverWorkHood
{
    public int DiscoverdPercent { get; set; }

    public (int x, int y) Position { get; set; }
}