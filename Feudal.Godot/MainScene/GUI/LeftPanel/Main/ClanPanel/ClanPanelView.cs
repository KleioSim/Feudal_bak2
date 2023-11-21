﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class ClanPanelView : ViewControl, IMainPanelView
{
    internal const string DefaultId = "Clan_DEFAULT";

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");
    public Label PopCount => GetNode<Label>("DataContainer/VBoxContainer/HBoxContainer/Pop/Value");
    public Label LaborCount => GetNode<Label>("DataContainer/VBoxContainer/HBoxContainer/Labor/Value");

    public string ClanId { get; set; } = DefaultId;
}

public interface IMainPanelView
{
    internal static Type[] DerivedTypes { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.IsAssignableTo(typeof(IMainPanelView))
            && x.IsClass && !x.IsAbstract)
        .ToArray();
}