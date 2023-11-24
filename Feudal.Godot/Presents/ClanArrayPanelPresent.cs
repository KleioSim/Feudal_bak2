using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

internal partial class ClanArrayPanelPresent : Present<ClanArrayPanelView, ISession>
{
    protected override void InitialConnects()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Refresh()
    {
        view.Container.Refresh(model.Clans.Select(x => x.Id as object).ToHashSet());
    }
}