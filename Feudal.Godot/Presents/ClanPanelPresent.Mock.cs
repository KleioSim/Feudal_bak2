using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

public partial class ClanPanelPresent : Present<ClanPanelView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new List<ClanMock>() { new ClanMock() { Id = "Clan_Mock", Name = "Clan_Mock_Name" } }
    };
}

public class ClanMock : IClan
{
    public static int Count;

    public string Id { get; set; }

    public string Name { get; set; }

    public int PopCount { get; set; }

    public ClanMock()
    {
        Id = $"Clan_{Count++}";

        Name = Id;
    }
}