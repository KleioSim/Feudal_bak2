using Godot;
using System;

public partial class GUIView : ViewControl
{
    public Button PlayerButton => GetNode<Button>("Top/Player");
    public Label PlayerClanName => GetNode<Label>("Top/Player/VBoxContainer/PlayerClanName");
    public Label PlayerClanPopCount => GetNode<Label>("Top/Player/VBoxContainer/PlayerClanPopCount");

    public Button ClansButton => GetNode<Button>("Top/VBoxContainer/TopInfoContainer/Clans");
    public Label ClansCount => GetNode<Label>("Top/VBoxContainer/TopInfoContainer/Clans/HBoxContainer/Value");

    public Button NextTurn => GetNode<Button>("Right/NextTurnButton");

    public InstancePlaceholder LeftPlaceHolder => GetNode<InstancePlaceholder>("LeftPlaceHolder");

    public override void _Ready()
    {
        base._Ready();

        PlayerButton.Pressed += () =>
        {
            var leftView = GetOrAddLeftView();
            leftView.ShowPlayerClan();
        };

        ClansButton.Pressed += () =>
        {
            var leftView = GetOrAddLeftView();
            leftView.ShowClans();
        };
    }

    private LeftView GetOrAddLeftView()
    {
        var leftView = LeftPlaceHolder.GetParent().GetNodeOrNull<LeftView>(nameof(LeftView));
        if (leftView == null)
        {
            leftView = LeftPlaceHolder.CreateInstance().GetNode<LeftView>(".");
            leftView.Name = nameof(LeftView);
        }

        return leftView;
    }
}
