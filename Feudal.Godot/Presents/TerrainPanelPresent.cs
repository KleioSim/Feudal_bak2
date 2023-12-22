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

        view.ResourceContainer.SetHidden(!terrain.IsDiscoverd);

        var workHood = model.WorkHoods.OfType<ITerrainWorkHood>()
            .SingleOrDefault(x => x.Position == terrain.Position);

        view.WorkHoodPanel.Id = workHood != null ? workHood.Id : null;

        //if (workHood == null)
        //{
        //    view.ClearWorkHoodPanel();
        //    return;
        //}

        //switch (workHood)
        //{
        //    case IDiscoverWorkHood:
        //        view.AddWorkHoodPanel<DiscoverWorkHoodPanelView>(workHood.Id);
        //        break;
        //    default:
        //        throw new Exception();
        //}
    }
}