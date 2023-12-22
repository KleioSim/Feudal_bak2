using Godot;
using System.Linq;

public partial class TerrainPanelView : MainPanelView
{
    public Vector2I TerrainPosition { get; set; }

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public Control ResourceContainer => GetNode<Control>("DataContainer/VBoxContainer/ResourceContainer");

    public WorkHoodPanelView WorkHoodPanel => GetNode<WorkHoodPanelView>("DataContainer/VBoxContainer/WorkPanel");

    public Control WorkHoodContainer => GetNode<Control>("DataContainer/VBoxContainer/WorkHoodContainer");
}
