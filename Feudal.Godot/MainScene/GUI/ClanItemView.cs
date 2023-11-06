using Godot;
using System;

public partial class ClanItemView : ItemView
{
    internal const string DefaultId = "Clan_DEFAULT";

    public static Action<string> ShowClan;

    public Label Label => GetNode<Label>("Button/HBoxContainer/Label");
    public Label Type => GetNode<Label>("Button/HBoxContainer/Type");
    public Label PopCount => GetNode<Label>("Button/HBoxContainer/Pop");

    public Button Button => GetNode<Button>("Button");

    public override string Id { get; set; } = DefaultId;

    public override void _Ready()
    {
        Button.Pressed += ()=> ShowClan(Id);

        base._Ready();
    }
}
