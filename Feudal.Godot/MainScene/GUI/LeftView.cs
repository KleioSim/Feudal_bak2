using Godot;
using System;

public partial class LeftView : ViewControl
{
    public Button Close => GetNode<Button>("HBoxContainer/Main/HBoxContainer/ControlContainer/XButton");

    public InstancePlaceholder ClanPanlPlaceHolder => GetNode<InstancePlaceholder>("HBoxContainer/Main/HBoxContainer/Content/ClanPanel");

    public override void _Ready()
    {
        Close.Pressed += () =>
        {
            this.QueueFree();
        };

        base._Ready();
    }

    internal void ShowClans()
    {
        //throw new NotImplementedException();
    }

    internal void ShowPlayerClan()
    {
        var clanPanelView = ClanPanlPlaceHolder.GetParent().GetNode<ClanPanelView>(nameof(ClanPanelView));
        if(clanPanelView == null)
        {
            clanPanelView = ClanPanlPlaceHolder.CreateInstance() as ClanPanelView;
        }

        //throw new NotImplementedException();
    }
}
