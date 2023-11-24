using Godot;
using System.Linq;

public partial class SubPanelContainer : Control
{
    internal T AddSubPanel<T>() where T : SubPanelView
    {
        this.SetHidden(false);

        var subPanels = GetChildren().OfType<SubPanelView>().ToList();
        foreach (var panel in subPanels)
        {
            panel.QueueFree();
        }

        var subPanel = GetNode<InstancePlaceholder>(typeof(T).Name).CreateInstance() as T;

        MoveChild(subPanel, -1);

        return subPanel;
    }
}