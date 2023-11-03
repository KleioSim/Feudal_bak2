using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

internal partial class ClanItemPresent : Present<ClanItemView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new List<ClanMock>() { new ClanMock() { Id = "Clan_DEFAULT", Name = "Clan_DEFAULT_Name" } }
    };

    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var clan = model.Clans.Single(x => x.Id == view.Id);
        view.Label.Text = clan.Name;
    }
}
