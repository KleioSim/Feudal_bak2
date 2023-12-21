using Godot;

public partial class WorkHoodPanelView : ViewControl
{
    private string id;
    public string Id
    {
        get => id;
        set
        {
            id = value;
            WorkingContainer.WorkHoodId = value;
        }
    }

    public Control LaborPanel => GetNode<Control>("PanelContainer/VBoxContainer/LaborPanel");
    public Control SelectLabor => LaborPanel.GetNode<Control>("SelectLabor");
    public Button SelectLaborButton => SelectLabor.GetNode<Button>("Button");

    public Control CurrentLabor => LaborPanel.GetNode<Control>("CurrentLabor");
    public Button CancelLaborButton => CurrentLabor.GetNode<Button>("CancelLabor");
    public Label ClanName => CurrentLabor.GetNode<Label>("Label");

    public WorkingContainerView WorkingContainer => GetNode<WorkingContainerView>("PanelContainer/VBoxContainer/WorkingContainer");

    [Signal]
    public delegate void AssginLaborEventHandler(string laborId);
}