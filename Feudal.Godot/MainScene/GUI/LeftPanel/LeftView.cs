using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

public partial class LeftView : ViewControl
{

    public MainPanelContainer MainPanelContainer => GetNode<MainPanelContainer>("HBoxContainer/Main/HBoxContainer");
    public SubPanelContainer SubPanelContainer => GetNode<SubPanelContainer>("HBoxContainer/Sub/HBoxContainer");


    public override void _Ready()
    {
        MainPanelContainer.Close.Pressed += CloseAllPanel;
        MainPanelContainer.Next.Pressed += CloseSubPanel;
        MainPanelContainer.Prev.Pressed += CloseSubPanel;

        SubPanelContainer.Close.Pressed += CloseSubPanel;

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

        manPanel.SelectLabor += () =>
        {
            var subPanel = SubPanelContainer.AddSubPanel<SelectLaborPanelView>();
        };

        return manPanel;
    }

    internal void CloseAllPanel()
    {
        MainPanelContainer.Clear();
        SubPanelContainer.Clear();

        MainPanelContainer.SetHidden(true);
        SubPanelContainer.SetHidden(true);
    }

    private void CloseSubPanel()
    {
        SubPanelContainer.Clear();
        SubPanelContainer.SetHidden(true);
    }
}
