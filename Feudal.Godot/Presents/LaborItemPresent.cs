using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class LaborItemPresent : Present<LaborItemView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var clan = model.Clans.SingleOrDefault(x => x.Id == view.Id);

        view.ClanName.Text = clan.Name;
        view.Count.Text = clan.Labor.TotalCount.ToString();
    }
}