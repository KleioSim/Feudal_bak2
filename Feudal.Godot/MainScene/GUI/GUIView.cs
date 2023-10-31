using Godot;
using System;

public partial class GUIView : View
{
    public Label PlayerClanName => GetNode<Label>("Top/Player/VBoxContainer/PlayerClanName");
    public Label PlayerClanPopCount => GetNode<Label>("Top/Player/VBoxContainer/PlayerClanPopCount");

    public Button NextTurn => GetNode<Button>("Right/NextTurnButton");
}
