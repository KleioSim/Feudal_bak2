using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskContainerView : ItemContainer<TaskItemView>
{
    public override InstancePlaceholder ItemPlaceHolder => GetNode<InstancePlaceholder>("TaskContainer/DefaultItem");
}