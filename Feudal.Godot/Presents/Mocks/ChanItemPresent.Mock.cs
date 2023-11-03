using Feudal.Interfaces;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

internal partial class ClanItemPresent : Present<ClanItemView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new List<ClanMock>() { new ClanMock() { Id = "Clan_DEFAULT", Name = "Clan_DEFAULT_Name" } }
    };
}
