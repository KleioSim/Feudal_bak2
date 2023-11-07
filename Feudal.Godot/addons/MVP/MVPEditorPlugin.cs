#if TOOLS
using Godot;
using System;
using System.Data;
using System.IO;
using System.Linq;

[Tool]
public partial class MVPEditorPlugin : EditorPlugin
{
	public override void _EnterTree()
	{
        // Initialization of the plugin goes here.
        // Add the new type with a name, a parent type, a script and an icon.
        var script = GD.Load<Script>("res://addons/MVP/ViewControl.cs");
        var texture = GD.Load<Texture2D>("res://addons/MVP/Icon.png");
        AddCustomType("ViewControl", "Control", script, texture);

        script = GD.Load<Script>("res://addons/MVP/ViewNode2D.cs");
        AddCustomType("ViewNode2D", "Node2D", script, texture);
    }


	public override void _ExitTree()
	{
        // Clean-up of the plugin goes here.
        // Always remember to remove it from the engine when deactivated.
        RemoveCustomType("ViewControl");
        RemoveCustomType("ViewNode2D");
    }
}
#endif
