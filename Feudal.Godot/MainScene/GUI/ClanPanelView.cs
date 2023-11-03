using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

//[LeftMainPanel(MainPanel.ClanPanel)]
public partial class ClanPanelView : MainPanelView
{
    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public string ClanId { get; set; } = "Clan_Mock";
}

public partial class MainPanelView : ViewControl
{
    internal static Type[] DerivedTypes { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.BaseType == typeof(MainPanelView))
        .ToArray();
}