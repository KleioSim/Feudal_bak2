using Feudal.Interfaces;
using Feudal.Messages;
using System;
using System.Linq;
using System.Reflection.Emit;

namespace Feudal.Godot.Presents;

internal partial class TerrainPanelPresent : Present<TerrainPanelView, ISession>
{
    protected override void InitialConnects()
    {
    }

    protected override void Refresh()
    {
        var terrain = model.Terrains.SingleOrDefault(x => x.Position == (view.TerrainPosition.X, view.TerrainPosition.Y));

        view.Title.Text = terrain.TerrainType.ToString();

        view.BufferContainer.SetHidden(!terrain.IsDiscoverd);

        if (terrain.WorkHoodId == null)
        {
            view.ClearWorkHoodPanel();
            return;
        }

        var workHood = model.WorkHoods.Single(x => x.Id == terrain.WorkHoodId);
        switch (workHood)
        {
            case IDiscoverWorkHood:
                view.AddWorkHoodPanel<DiscoverWorkHoodPanelView>(workHood.Id);
                break;
            default:
                throw new Exception();
        }
    }
}