using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class GUIPresent : Present<GUIView, ISession>
{
    protected override void InitialConnects()
    {
        view.NextTurn.ButtonDown += () => SendUICommand(new NextTurnCommand());
    }

    protected override void Process()
    {
        view.PlayerClanName.Text = model.PlayerClan.Name;
        view.PlayerClanPopCount.Text = model.PlayerClan.PopCount.ToString();

        view.ClanCount.Text = model.Clans.Count().ToString();
    }
}


public class ClanMock : IClan
{
    public string Id { get; }

    public string Name { get; set; }

    public int PopCount { get; set; }
}