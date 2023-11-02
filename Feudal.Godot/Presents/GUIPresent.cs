using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

public partial class GUIPresent : Present<GUIView, ISession>
{
    protected override ISession MockModel => new SessionMock()
    {
        PlayerClanName = "Mock",
        PlayerClanPopCount = 0,

        tasks = new List<TaskMock>()
        {
            new TaskMock()
            {
                Id = "TASK1"
            },
            new TaskMock()
            {
                Id = "TASK2"
            },
        }
    };

    protected override void InitialConnects()
    {
        view.NextTurn.ButtonDown += () => SendUICommand(new NextTurnCommand());
    }

    protected override void Process()
    {
        view.PlayerClanName.Text = model.PlayerClanName;
        view.PlayerClanPopCount.Text = model.PlayerClanPopCount.ToString();
    }
}

public class SessionMock : ISession
{
    public string PlayerClanName { get; set; }
    public int PlayerClanPopCount { get; set; }

    public IEnumerable<ITask> tasks { get; set; }

    public void ProcessUICommand(UICommand command)
    {
        
    }
}