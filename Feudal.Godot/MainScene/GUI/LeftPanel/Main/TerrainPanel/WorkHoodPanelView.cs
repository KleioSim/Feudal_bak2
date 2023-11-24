using Godot;
using System;

public partial class WorkHoodPanelView : ViewControl
{
    public const string DefaultId = "DefaultId";

    public string Id { get; private set; } = DefaultId;

    public Control Product => GetNode<Control>("VBoxContainer/Product");

    public ProgressBar DiscoverProgress => GetNode<ProgressBar>("VBoxContainer/DiscoverProgressBar");
    public Label DiscoverLabel => DiscoverProgress.GetNode<Label>("Label");

    public Control SelectLabor => GetNode<Control>("VBoxContainer/LaborPanel/SelectLabor");
    public Button SelectLaborButton => SelectLabor.GetNode<Button>("Button");

    public Control CurrentLabor => GetNode<Control>("VBoxContainer/LaborPanel/CurrentLabor");
    public Button CancelLaborButton => CurrentLabor.GetNode<Button>("CancelLabor");

    internal Action<string> AssignLabor;

    internal void SetWorkHoodId(string Id)
    {
        this.Id = Id;
        this.SetHidden(Id == null);
    }
}
