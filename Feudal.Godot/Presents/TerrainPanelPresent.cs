using Feudal.Interfaces;
using System.Linq;

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
    }
}