using Godot;
using System;

public partial class ClanItemView : ItemView
{
    internal const string DefaultId = "Clan_DEFAULT";

    public Label Label => GetNode<Label>("Button/HBoxContainer/Label");
    public Button Progress => GetNode<Button>("Button");

    public override string Id { get; set; } = DefaultId;
}
