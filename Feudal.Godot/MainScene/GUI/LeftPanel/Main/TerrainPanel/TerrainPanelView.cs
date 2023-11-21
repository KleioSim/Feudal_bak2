using Godot;
using System;

public partial class TerrainPanelView : ViewControl, IMainPanelView
{
    public static readonly Vector2I DefaultPos = new Vector2I(0, 0);

    public Vector2I TerrainPosition { get; set; } = DefaultPos;

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public Control BufferContainer => GetNode<Control>("DataContainer/VBoxContainer/BuffersPanel/BufferContainer");

    public Control Product => GetNode<Control>("DataContainer/VBoxContainer/WorkPanel/VBoxContainer/Product");

    public ProgressBar DiscoverProgress => GetNode<ProgressBar>("DataContainer/VBoxContainer/BuffersPanel/DiscoverProgressBar");
    public Label DiscoverLabel => DiscoverProgress.GetNode<Label>("Label");

    public Control WorkPanel => GetNode<Control>("DataContainer/VBoxContainer/WorkPanel");
    public Control SelectLabor => WorkPanel.GetNode<Control>("VBoxContainer/LaborPanel/SelectLabor");
    public Control CurrentLabor => WorkPanel.GetNode<Control>("VBoxContainer/LaborPanel/CurrentLabor");
    public Button CancelLabor => CurrentLabor.GetNode<Button>("CancelLabor");
}
