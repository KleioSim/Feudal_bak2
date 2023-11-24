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
                view.ShowClanPanel(ClanItemView.DefaultId);
            }
            else if (mainPanelType == nameof(TerrainPanelView))
            {
                view.ShowTerrainPanel(TerrainPanelView.DefaultPos);
            }
            else if (mainPanelType == "NULL")
            {
                view.SetHidden(true);
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

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new[]
        {
            new ClanMock(){ Id = ClanItemView.DefaultId, Name = ClanItemView.DefaultId, PopCount = 1000 },
            new ClanMock(),
            new ClanMock(),
        },
        Terrains = new[]
        {
            new TerrainMock(){ Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y), TerrainType = TerrainType.Hill, WorkHoodId = WorkHoodPanelView.DefaultId }
        },
        WorkHoods = new[]
        {
            new DiscoverWorkHood_Mock(){ Id = WorkHoodPanelView.DefaultId }
        }
    };
}
