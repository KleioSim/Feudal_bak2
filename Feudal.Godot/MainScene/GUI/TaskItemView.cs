using Godot;
using System;

public partial class TaskItemView : ItemView
{
    public Label Label => GetNode<Label>("VBoxContainer/Label");
    public ProgressBar Progress => GetNode<ProgressBar>("VBoxContainer/ProgressBar");

    public override object Id { get; set; } = "TASK_DEFAULT";
}

public abstract partial class ItemView : ViewControl, IItemView
{
    public abstract object Id { get; set; }
}

public interface IItemView
{
    object Id { get; set; }
}

public static class ControlExtension
{
    public static void SetHidden(this Control control, bool flag)
    {
        control.SetProcess(!flag);
        control.Visible = !flag;
    }
}