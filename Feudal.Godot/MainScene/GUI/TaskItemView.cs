using Godot;
using System;

public partial class TaskItemView : ItemView
{
    public Label Label => GetNode<Label>("VBoxContainer/Label");
    public ProgressBar Progress => GetNode<ProgressBar>("VBoxContainer/ProgressBar");

    public override string Id { get; set; } = "TASK_DEFAULT";
}

public abstract partial class ItemView : ViewControl
{
    public abstract string Id { get; set; }

    public void SetHidden(bool flag)
    {
        SetProcess(!flag);
        Visible = !flag;
    }
}