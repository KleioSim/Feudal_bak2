using Godot;
using System;

public partial class TerrainPanelView : ViewControl, IMainPanelView
{
    public static readonly Vector2I DefaultPos = new Vector2I(0, 0);

    public Vector2I TerrainPosition { get; set; } = DefaultPos;

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");
}
