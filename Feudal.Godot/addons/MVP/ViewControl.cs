using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

[Tool]
public partial class ViewControl : Control
{
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

            var present = GetNodeOrNull(nameof(Present)) as Present;
            if (present == null)
            {
                present = Activator.CreateInstance(presentType) as Present;
                present.Name = nameof(Present);

                AddChild(present, false, InternalMode.Front);
            }

            present.SetView(this);
        }

        base._Ready();
    }
}
