using Godot;
using System;

public partial class TaskItemView : ViewControl
{
    public Label Label => GetNode<Label>("VBoxContainer/Label");
    public ProgressBar Progress => GetNode<ProgressBar>("VBoxContainer/ProgressBar");

    public string taskId { get; set; } = "TASK_DEFAULT";

    public void SetHidden(bool flag)
    {
        SetProcess(!flag);
        Visible = !flag;
    }
}
