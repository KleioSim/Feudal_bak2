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

    protected override void Refresh()
    {
        view.Year.Text = model.Date.Year.ToString();
        view.Month.Text = model.Date.Month.ToString();
        view.Day.Text = model.Date.Day.ToString();

        view.PlayerClanId = model.PlayerClan.Id;

        view.PlayerClanName.Text = model.PlayerClan.Name;
        view.PlayerClanPopCount.Text = model.PlayerClan.PopCount.ToString();

        view.ClansCount.Text = model.Clans.Count().ToString();
    }
}