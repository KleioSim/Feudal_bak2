using Godot;
using System;
using System.Collections.Generic;

public partial class LeftView : ViewControl
{
    public Button Close => GetNode<Button>("HBoxContainer/Main/HBoxContainer/ControlContainer/XButton");

    public InstancePlaceholder ClanPanelPlaceHolder => GetNode<InstancePlaceholder>("HBoxContainer/Main/HBoxContainer/Content/ClanPanel");
    public InstancePlaceholder ClanArrayPanelPlaceHolder => GetNode<InstancePlaceholder>("HBoxContainer/Main/HBoxContainer/Content/ClanArrayPanel");

    public override void _Ready()
    {
        Close.Pressed += () =>
        {
            this.QueueFree();
        };

        base._Ready();
    }


    internal T ShowMainPanel<T>() where T : ViewControl, IMainPanelView
    {
        return (T)ShowMainPanel(typeof(T));
    }

    internal ViewControl ShowMainPanel(Type type)
    {
        InstancePlaceholder placeHolder = null;

        if (type == typeof(ClanPanelView))
        {
            placeHolder = ClanPanelPlaceHolder;
        }
        else if(type == typeof(ClanArrayPanelView))
        {
            placeHolder = ClanArrayPanelPlaceHolder;
        }
        else
        {
            throw new Exception();
        }

        var childCount = placeHolder.GetParent().GetChildCount();
        for(int i = 0; i<childCount; i++)
        {
            var child = placeHolder.GetParent().GetChild(i);
            if(!(child is InstancePlaceholder))
            {
                child.QueueFree();
            }
        }

        var mainPanelView = placeHolder.GetParent().GetNodeOrNull(type.Name) as ViewControl;
        if (mainPanelView == null)
        {
            mainPanelView = placeHolder.CreateInstance() as ViewControl;
            mainPanelView.Name = type.Name;
        }

        return mainPanelView;
    }
}