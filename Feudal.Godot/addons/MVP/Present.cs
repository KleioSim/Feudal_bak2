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

    protected IView view;

    protected abstract void InitialConnects();
    protected abstract void Process();

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

            Process();
        }
    }

    internal void SetView(IView view)
    {
        if (this.view != null)
        {
            throw new Exception();
        }

        this.view = view;
        InitialConnects();
    }
}

public abstract partial class Present<TView, TModel> : Present
    where TView : class, IView
    where TModel : class, ISession
{
    protected abstract TModel MockModel { get; }

    protected new TModel model { get; private set; }
    protected new TView view => base.view as TView;

    protected abstract void Refresh();

    protected override void Process()
    {
        model = (Present.model ??= MockModel) as TModel;

        Refresh();
    }
}