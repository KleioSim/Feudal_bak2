using Feudal.Interfaces;
using Feudal.Messages;
using Godot;

public partial class LeftView : ViewControl
{

    public MainPanelContainer MainPanelContainer => GetNode<MainPanelContainer>("HBoxContainer/Main/HBoxContainer");
    public SubPanelContainer SubPanelContainer => GetNode<SubPanelContainer>("HBoxContainer/Sub/HBoxContainer");


    public override void _Ready()
    {
        MainPanelContainer.Close.Pressed += CloseAllPanel;

        MainPanelContainer.Next.Pressed += () => SubPanelContainer.ClosePanel();
        MainPanelContainer.Prev.Pressed += () => SubPanelContainer.ClosePanel();
        SubPanelContainer.Close.Pressed += () => SubPanelContainer.ClosePanel();

        SubPanelContainer.SetHidden(true);

        ClanItemView.ShowClan = (clanId) => ShowClanPanel(clanId);
        base._Ready();
    }

    internal ClanPanelView ShowClanPanel(string clanId)
    {
        var manPanel = MainPanelContainer.AddOrFindMainPanel<ClanPanelView>(x => x.ClanId == clanId);
        manPanel.ClanId = clanId;

        return manPanel;
    }

    internal ClanArrayPanelView ShowClanArrayPanel()
    {
        var manPanel = MainPanelContainer.AddOrFindMainPanel<ClanArrayPanelView>();
        return manPanel;
    }

    internal TerrainPanelView ShowTerrainPanel(Vector2I pos)
    {
        var manPanel = MainPanelContainer.AddOrFindMainPanel<TerrainPanelView>(x => x.TerrainPosition == pos);
        manPanel.TerrainPosition = pos;

        manPanel.WorkHoodPanel2.SelectLaborButton.Pressed += () =>
        {
            var subPanel = SubPanelContainer.AddSubPanel<SelectLaborPanelView>();
            subPanel.SelectedLabor += (Id) =>
            {
                SubPanelContainer.ClosePanel();
                manPanel.WorkHoodPanel2.EmitSignal(LaborInWorkHoodView.SignalName.AssginLabor, Id);
            };
        };

        return manPanel;
    }

    internal void CloseAllPanel()
    {
        MainPanelContainer.ClosePanel();
        SubPanelContainer.ClosePanel();
    }
}
