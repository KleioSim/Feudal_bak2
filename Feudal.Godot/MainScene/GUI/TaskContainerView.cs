using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskContainerView : ViewControl
{
    public TaskView DefaultItem => GetNode<TaskView>("TaskContainer/DefaultItem");

    public IEnumerable<TaskView> GetCurrentItems()
    {
        return DefaultItem.GetParent().GetChildren().Select(x => x.GetNodeOrNull<TaskView>("."))
            .Where(x => x != null);
    }

    public TaskView GenerateItem(string taskId)
    {
        var item = DefaultItem.Duplicate().GetNode<TaskView>(".");
        item.taskId = taskId;

        DefaultItem.GetParent().AddChild(item);
        item.SetHidden(false);

        return item;
    }

    public void RemoveItem(TaskView item)
    {
        if(!DefaultItem.GetParent().GetChildren().Contains(item))
        {
            throw new Exception("!DefaultItem.GetParent().GetChildren().Contains(item)");
        }

        item.SetHidden(true);
    }
}