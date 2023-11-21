using Feudal.Interfaces;
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

        view.WorkPanel.SetWorkHoodId(terrain.WorkHoodId);
    }
}