using Godot;
using System;

public partial class GUIView : ViewControl
{
    public Label PlayerClanName => GetNode<Label>("Top/Player/VBoxContainer/PlayerClanName");
    public Label PlayerClanPopCount => GetNode<Label>("Top/Player/VBoxContainer/PlayerClanPopCount");

    public Label ClanCount => GetNode<Label>("Top/VBoxContainer/TopInfoContainer/Clan/HBoxContainer/Value");

    public Button NextTurn => GetNode<Button>("Right/NextTurnButton");
}
