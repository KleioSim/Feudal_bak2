using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskRightPanelView : ViewControl
{
    internal ItemContainer<TaskItemView> Container;

    public TaskRightPanelView()
    {
        Container = new ItemContainer<TaskItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("TaskContainer/DefaultItem");
        });
    }
}