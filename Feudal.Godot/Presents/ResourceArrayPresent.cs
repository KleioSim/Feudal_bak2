using Feudal.Interfaces;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ResourceArrayPresent : Present<ResourceArrayView, ISession>
{
    protected override void InitialConnects()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Refresh()
    {
        var terrain = model.Terrains.SingleOrDefault(x => x.Position == view.TerrainPos);

        view.Container.Refresh(terrain.Resources.OfType<object>().ToHashSet());
    }
}