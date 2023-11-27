using Godot;
using System.Linq;

public partial class TerrainPanelView : MainPanelView
{
    public static readonly Vector2I DefaultPos = new Vector2I(0, 0);

    public Vector2I TerrainPosition { get; set; } = DefaultPos;

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public Control BufferContainer => GetNode<Control>("DataContainer/VBoxContainer/BufferContainer");

    public Control WorkHoodContainer => GetNode<Control>("DataContainer/VBoxContainer/WorkHoodContainer");

    public IWorkHoodPanel WorkHoodPanel => WorkHoodContainer.GetChildren().SingleOrDefault(x => x is IWorkHoodPanel) as IWorkHoodPanel;

    internal void ClearWorkHoodPanel()
    {
        if (WorkHoodPanel != null)
        {
            ((Control)WorkHoodPanel).QueueFree();
        }
    }

    internal void AddWorkHoodPanel<T>(string Id)
        where T : class, IWorkHoodPanel
    {
        if (WorkHoodPanel != null)
        {
            WorkHoodPanel.Id = Id;
            return;
        }

        var placeHolder = WorkHoodContainer.GetNode<InstancePlaceholder>(typeof(T).Name);
        var workHoodPanel = placeHolder.CreateInstance() as T;
        workHoodPanel.Id = Id;
    }
}
