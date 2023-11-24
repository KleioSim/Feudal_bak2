using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class LaborItemPresent : Present<LaborItemView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new List<ClanMock>() { new ClanMock() { Id = LaborItemView.DefaultId, Name = "Clan_DEFAULT_Name", PopCount = 1000 } }
    };

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