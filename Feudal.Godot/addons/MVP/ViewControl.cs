using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

[Tool]
public partial class ViewControl : Control
{
    [Export]
    public int tests;

    public static Dictionary<Type, Type> dict { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.BaseType.IsGenericType
             && x.BaseType.GetGenericTypeDefinition() == typeof(Present<,>))
        .ToDictionary(x => x.BaseType.GetGenericArguments()[0], x => x);

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            if(!dict.TryGetValue(this.GetType(), out Type presentType))
            {
                GD.PushWarning($"Can not find present for {this.GetType()}");
                return;
            }

            var present = Activator.CreateInstance(presentType) as Present;
            AddChild(present);

            present.SetView(this);
        }

        base._Ready();
    }
}
