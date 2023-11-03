using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class ClanPanelView : ViewControl, IMainPanelView
{
    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public string ClanId { get; set; } = "Clan_DEFAULT";
}

public interface IMainPanelView
{
    internal static Type[] DerivedTypes { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x =>x.IsAssignableTo(typeof(IMainPanelView)) 
            && x.IsClass && !x.IsAbstract)
        .ToArray();
}