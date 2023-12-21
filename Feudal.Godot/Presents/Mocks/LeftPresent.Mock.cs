using Feudal.Interfaces;
using Godot.Collections;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

[Tool]
public partial class LeftPresent : Present<LeftView, ISession>
{
    private string mainPanelType = "NULL";
    public string MainPanelType
    {
        get => mainPanelType;
        set
        {
            if (mainPanelType == value)
            {
                return;
            }

            mainPanelType = value;

            if (mainPanelType == nameof(ClanArrayPanelView))
            {
                view.ShowClanArrayPanel();
            }
            else if (mainPanelType == nameof(ClanPanelView))
            {
                view.ShowClanPanel(model.Clans.First().Id);
            }
            else if (mainPanelType == nameof(TerrainPanelView))
            {
                var terrain = model.Terrains.First();
                view.ShowTerrainPanel(new Vector2I(terrain.Position.x, terrain.Position.y));
            }
            else if (mainPanelType == "NULL")
            {
                view.CloseAllPanel();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        var MainPanelTypes = MainPanelView.DerivedTypes.Select(x => x.Name).ToArray();

        properties.Add(new Dictionary()
        {
            { "name", nameof(MainPanelType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", MainPanelTypes.Append("NULL")) }
        });

        return properties;
    }


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

            var clans = new[]
            {
                new ClanMock(){ PopCount = 1000 },
                new ClanMock(),
                new ClanMock(),
            };

            var task = new TaskMock()
            {
                WorkHoodId = workHood.Id,
                ClanId = clans[0].Id
            };

            var mock = new SessionMock();

            mock.TerrainMocks.Add(terrain);
            mock.WorkHoodMocks.Add(workHood);
            mock.ClanMocks.AddRange(clans);

            mock.TaskMocks.Add(task);

            return mock;
        }
    }

    //{ get; } = new SessionMock()
    //{
    //    //Clans = new[]
    //    //{
    //    //    new ClanMock(){ Id = ClanItemView.DefaultId, Name = ClanItemView.DefaultId, PopCount = 1000 },
    //    //    new ClanMock(),
    //    //    new ClanMock(),
    //    //},
    //    //Terrains = new[]
    //    //{
    //    //    new TerrainMock(){ Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y), TerrainType = TerrainType.Hill}
    //    //},
    //    //WorkHoods = new[]
    //    //{
    //    //    new DiscoverWorkHood_Mock(){ Id = DiscoverWorkHoodPanelView.DefaultId, Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y)}
    //    //}
    //};
}
