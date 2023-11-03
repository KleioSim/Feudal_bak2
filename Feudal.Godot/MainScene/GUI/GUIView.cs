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

    public string PlayerClanId { get; set; }

    public override void _Ready()
    {
        base._Ready();

        PlayerButton.Pressed += () =>
        {
            var leftView = GetOrAddLeftView();

            var clanPanelView = leftView.ShowMainPanel<ClanPanelView>();
            clanPanelView.ClanId = PlayerClanId;
        };

        ClansButton.Pressed += () =>
        {
            var leftView = GetOrAddLeftView();

            var clanPanelView = leftView.ShowMainPanel<ClanPanelView>();
            clanPanelView.ClanId = PlayerClanId;
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
