using Godot;
using System;
using System.Collections.Generic;

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


    internal T ShowMainPanel<T>() where T:MainPanelView
    {
        return ShowMainPanel(typeof(T)) as T;
    }

    internal MainPanelView ShowMainPanel(Type type)
    {
        InstancePlaceholder placeHolder = null;

        if (type == typeof(ClanPanelView))
        {
            placeHolder = ClanPanlPlaceHolder;
        }
        else
        {
            throw new Exception();
        }

        var mainPanelView = placeHolder.GetParent().GetNodeOrNull(type.Name) as MainPanelView;
        if (mainPanelView == null)
        {
            mainPanelView = placeHolder.CreateInstance() as MainPanelView;
            mainPanelView.Name = type.Name;
        }

        return mainPanelView;
    }
}