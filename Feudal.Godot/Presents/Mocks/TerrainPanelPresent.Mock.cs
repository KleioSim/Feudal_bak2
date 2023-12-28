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
    protected override ISession MockModel
    {
        get
        {
            var terrain = new TerrainMock()
            {
                Position = (0, 0),
                TerrainType = Interfaces.TerrainType.Plain,
                Resources = new[] { Interfaces.Resource.FatSoild },
                IsDiscoverd = false,
            };

            var workHood = new TerrainWorkHoodMock() { Position = terrain.Position };
            workHood.OptionWorkingMocks.AddRange(new IWorking[]
            {
                new ProgressWorking_Mock(){ Percent = 20 },
                new ProductWorking_Mock() { ProductType = ProductType.Food, ProductCount = 1.0 },
            });
            workHood.CurrentWorking = workHood.OptionWorkingMocks.First();

            view.TerrainPosition = new Vector2I(terrain.Position.x, terrain.Position.y);

            var clan = new ClanMock();
            var task = new TaskMock()
            {
                WorkHoodId = workHood.Id,
                ClanId = clan.Id
            };

            var mock = new SessionMock();

            mock.TerrainMocks.Add(terrain);
            mock.WorkHoodMocks.Add(workHood);
            mock.ClanMocks.Add(clan);
            mock.TaskMocks.Add(task);

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

    public IWorking CurrentWorking { get; set; }

    public IEnumerable<IWorking> OptionWorkings => OptionWorkingMocks;

    public List<IWorking> OptionWorkingMocks { get; } = new List<IWorking>();
}

public class TerrainWorkHoodMock : WorkHood_Mock, ITerrainWorkHood
{
    public (int x, int y) Position { get; set; }
}