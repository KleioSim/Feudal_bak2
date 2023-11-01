using Godot;
using System;

public partial class TaskView : ViewControl
{
    public string taskId { get; set; } = "DEFAULT";

    public void SetHidden(bool flag)
    {
        SetProcess(!flag);
        Visible = !flag;
    }
}
