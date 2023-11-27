using Godot;
using System;

public partial class DiscoverWorkHoodPanelView : ViewControl, IWorkHoodPanel
{
    public const string DefaultId = "DefaultId";

    private string id = DefaultId;
    public string Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            LaborManager.SetWorkHoodId(id);
        }
    }

    public ProgressBar DiscoverProgress => GetNode<ProgressBar>("VBoxContainer/DiscoverProgressBar");
    public Label DiscoverLabel => DiscoverProgress.GetNode<Label>("Label");

    public LaborInWorkHoodView LaborManager => GetNode<LaborInWorkHoodView>("VBoxContainer/LaborPanel");
}


public interface IWorkHoodPanel
{
    string Id { get; set; }

    LaborInWorkHoodView LaborManager { get; }
}