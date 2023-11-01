using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


public abstract partial class Present : Control
{
    private static HashSet<Present> list = new HashSet<Present>();

    public static ISession model { get; set; }

    protected bool isDirty { get; set; } = true;

    private ViewControl view;

    protected abstract void InitialConnects(ViewControl view);
    protected abstract void Process(ViewControl view, object model);

    internal void SendUICommand(UICommand command)
    {
        model.ProcessUICommand(command);

        foreach (var item in list)
        {
            item.isDirty = true;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        base._ExitTree();

        isDirty = true;

        list.Add(this);
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        list.Remove(this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (isDirty)
        {
            isDirty = false;

            Process(view, model);
        }
    }

    internal void SetView(ViewControl view)
    {
        if (this.view != null)
        {
            throw new Exception();
        }

        this.view = view;
        InitialConnects(view);
    }
}

public abstract partial class Present<TView, TModel> : Present
    where TView : ViewControl
    where TModel : class
{
    public abstract TModel MockModel { get; }

    protected override void Process(ViewControl view, object model)
    {
        Refresh(view as TView, (model ?? MockModel) as TModel);
    }

    protected override void InitialConnects(ViewControl view)
    {
        InitialConnects(view as TView);
    }

    protected abstract void Refresh(TView view, TModel model);
    protected abstract void InitialConnects(TView view);
}