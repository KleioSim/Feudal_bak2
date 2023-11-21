using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Feudal.Messages;
using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class GUIPresent : Present<GUIView, ISession>
{
    protected override void InitialConnects()
    {
        view.NextTurn.ButtonDown += () => SendUICommand(new MESSAGE_NextTurn());
    }

    protected override void Refresh()
    {
        view.Year.Text = model.Date.Year.ToString();
        view.Month.Text = model.Date.Month.ToString();

        if (model.Date.Day > 20)
        {
            view.Day.Text = "下";
        }
        else if (model.Date.Day > 10)
        {
            view.Day.Text = "中";
        }
        else
        {
            view.Day.Text = "上";
        }

        view.PlayerClanId = model.PlayerClan.Id;

        view.PlayerClanName.Text = model.PlayerClan.Name;
        view.PlayerClanPopCount.Text = model.PlayerClan.PopCount.ToString();

        view.ClansCount.Text = model.Clans.Count().ToString();
    }
}
