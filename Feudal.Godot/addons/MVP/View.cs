using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

[Tool]
public partial class View : Node
{
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            var scene = GD.Load<PackedScene>("res://addons/MVP/present_manager.tscn");
            var presentMgr = scene.Instantiate<PresentManager>();
            presentMgr.Name = "PresentManager";

            presentMgr.SetView(this);

            AddChild(presentMgr);
        }

        base._Ready();
    }
}