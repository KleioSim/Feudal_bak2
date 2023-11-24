using Godot;

public partial class LaborItemView : ItemView
{
    public const string DefaultId = "Clan_DEFAULT";

    public override object Id { get; set; } = DefaultId;

    public Label ClanName => GetNode<Label>("Button/HBoxContainer/Label");
    public Label Count => GetNode<Label>("Button/HBoxContainer/Pop");

    public Button Button => GetNode<Button>("Button");
}