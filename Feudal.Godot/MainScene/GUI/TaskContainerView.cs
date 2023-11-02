using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskContainerView : ViewControl
{
    public InstancePlaceholder ItemPlaceHolder => GetNode<InstancePlaceholder>("TaskContainer/DefaultItem");

    public IEnumerable<TaskItemView> GetCurrentItems()
    {
        return ItemPlaceHolder.GetParent().GetChildren().Select(x => x.GetNodeOrNull<TaskItemView>("."))
            .Where(x => x != null);
    }

    public TaskItemView GenerateItem(string taskId)
    {
        var item = ItemPlaceHolder.CreateInstance() as TaskItemView;
        item.taskId = taskId;

        item.SetHidden(false);

        return item;
    }

    public void RemoveItem(TaskItemView item)
    {
        if(!ItemPlaceHolder.GetParent().GetChildren().Contains(item))
        {
            throw new Exception("!this.GetChildren().Contains(item)");
        }

        item.SetHidden(true);
    }
}