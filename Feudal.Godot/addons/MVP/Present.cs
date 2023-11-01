using Feudal.Interfaces.UICommands;
using Godot;
using System;

public abstract class Present
{
    internal void SendUICommand(UICommand command)
    {
        PresentManager.SendUICommand(command);
    }

    internal abstract void InitialConnects(ViewControl view);
    internal abstract void Process(ViewControl view, object model);
}

public abstract class Present<TView, TModel> : Present
    where TView : ViewControl
    where TModel : class
{
    public abstract TModel MockModel { get; }

    internal override void Process(ViewControl view, object model)
    {
        Refresh(view as TView, (model ?? MockModel) as TModel);
    }

    internal override void InitialConnects(ViewControl view)
    {
        InitialConnects(view as TView);
    }

    protected abstract void Refresh(TView view, TModel model);
    protected abstract void InitialConnects(TView view);
}
