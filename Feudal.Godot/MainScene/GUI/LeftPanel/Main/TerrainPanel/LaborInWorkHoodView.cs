using Godot;
using System;

public partial class LaborInWorkHoodView : ViewControl
{
    public Control SelectLabor => GetNode<Control>("SelectLabor");
    public Button SelectLaborButton => SelectLabor.GetNode<Button>("Button");

    public Control CurrentLabor => GetNode<Control>("CurrentLabor");
    public Button CancelLaborButton => CurrentLabor.GetNode<Button>("CancelLabor");
    public Label ClanName => CurrentLabor.GetNode<Label>("Label");

    [Signal]
    public delegate void AssginLaborEventHandler(string laborId);

    public string WorkHoodId { get; internal set; } = DiscoverWorkHoodPanelView.DefaultId;

    internal void SetWorkHoodId(string Id)
    {
        this.WorkHoodId = Id;
        this.SetHidden(WorkHoodId == null);
    }
}
