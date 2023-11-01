using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class PresentManager : Node
{
    public static Dictionary<Type, Present> dict { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.BaseType.IsGenericType
            && x.BaseType.GetGenericTypeDefinition() == typeof(Present<,>))
        .ToDictionary(x => x.BaseType.GetGenericArguments()[0], x => Activator.CreateInstance(x) as Present);

    public static ISession model { get; set; }

    private static HashSet<PresentManager> list = new HashSet<PresentManager>();

    public bool isDirty { get; set; } = true;

    private ViewControl view;
    private Present present;

    internal static void SendUICommand(UICommand command)
    {
        model.ProcessUICommand(command);

        foreach(var item in list)
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

            if(present == null)
            {
                GD.PushWarning($"Can not find present for {view.GetType()}");
                return;
            }

            present.Process(view, model);
        }
    }

    internal void SetView(ViewControl view)
    {
        if(this.view != null)
        {
            throw new Exception();
        }

        this.view = view;

        if (!dict.TryGetValue(view.GetType(), out present))
        {
            GD.PushWarning($"Can not find present for {view.GetType()}");
            return;
        }

        present.InitialConnects(view);
    }
}
