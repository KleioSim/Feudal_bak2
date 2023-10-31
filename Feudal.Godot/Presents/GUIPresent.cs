using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;

namespace Fedual.Presents;

public class GUIPresent : Present<GUIView, ISession>
{
    public override ISession MockModel => new SessionMock()
    {
        PlayerClanName = "Mock",
        PlayerClanPopCount = 0,
    };

    protected override void InitialConnects(GUIView view)
    {
        view.NextTurn.ButtonDown += () => SendUICommand(new NextTurnCommand());
    }

    protected override void Refresh(GUIView view, ISession model)
    {
        view.PlayerClanName.Text = model.PlayerClanName;
        view.PlayerClanPopCount.Text = model.PlayerClanPopCount.ToString();
    }
}

public class SessionMock : ISession
{
    public string PlayerClanName { get; set; }
    public int PlayerClanPopCount { get; set; }

    public void ProcessUICommand(UICommand command)
    {
        
    }
}