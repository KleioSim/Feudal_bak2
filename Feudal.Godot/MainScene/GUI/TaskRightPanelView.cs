using System;
using System.Collections.Generic;
using System.Linq;

public partial class TaskRightPanelView : ViewControl
{
    public TaskContainer Container => GetNode<TaskContainer>("TaskContainer");
}