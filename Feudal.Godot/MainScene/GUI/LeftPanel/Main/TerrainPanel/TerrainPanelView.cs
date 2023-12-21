using Godot;
using System.Linq;

public partial class TerrainPanelView : MainPanelView
{
    public Vector2I TerrainPosition { get; set; }

    public Label Title => GetNode<Label>("DataContainer/VBoxContainer/Title");

    public Control ResourceContainer => GetNode<Control>("DataContainer/VBoxContainer/ResourceContainer");

    public WorkHoodPanelView WorkHoodPanel2 => GetNode<WorkHoodPanelView>("DataContainer/VBoxContainer/WorkPanel");

    public Control WorkHoodContainer => GetNode<Control>("DataContainer/VBoxContainer/WorkHoodContainer");

    public IWorkHoodPanel WorkHoodPanel => WorkHoodContainer.GetChildren().SingleOrDefault(x => x is IWorkHoodPanel) as IWorkHoodPanel;

    //internal void ClearWorkHoodPanel()
    //{
    //    if (WorkHoodPanel != null)
    //    {
    //        ((Control)WorkHoodPanel).Free();
    //    }
    //}

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
