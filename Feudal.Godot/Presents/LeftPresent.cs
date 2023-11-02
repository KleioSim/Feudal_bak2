using Feudal.Interfaces;
using Godot;

namespace Feudal.Godot.Presents;

public partial class LeftPresent : Present<LeftView, ISession>
{
    public enum MainPanel
    {
        ClanPanel,
        ClansPanel,
    }

    [Export]
    MainPanel mainPanel { get; set; }

    protected override ISession MockModel => throw new System.NotImplementedException();

    protected override void InitialConnects()
    {

    }

    protected override void Process()
    {

    }
}