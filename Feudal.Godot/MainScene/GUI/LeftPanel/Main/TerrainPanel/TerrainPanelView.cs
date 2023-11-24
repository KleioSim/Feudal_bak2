using Godot;
using System;

public partial class TerrainPanelView : MainPanelView
{
    public static readonly Vector2I DefaultPos = new Vector2I(0, 0);

    public Vector2I TerrainPosition { get; set; } = DefaultPos;

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public Control BufferContainer => GetNode<Control>("DataContainer/VBoxContainer/BufferContainer");

    public WorkHoodPanelView WorkPanel => GetNode<WorkHoodPanelView>("DataContainer/VBoxContainer/WorkPanel");

    [Signal]
    public delegate void SelectLaborEventHandler();

    public override void _Ready()
    {
        base._Ready();

        WorkPanel.SelectLaborButton.Pressed += () =>
        {
            EmitSignal(SignalName.SelectLabor);
        };
    }

}
