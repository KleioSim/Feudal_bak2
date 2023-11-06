using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ClanPanelPresent : Present<ClanPanelView, ISession>
{
    protected override void InitialConnects()
    {
        
    }

    protected override void Refresh()
    {
        var clan = model.Clans.Single(x => x.Id == view.ClanId);

        view.Title.Text = clan.Name;
        view.PopCount.Text = clan.PopCount.ToString();
        view.LaborCount.Text = clan.Labor.TotalCount.ToString();
    }
}
