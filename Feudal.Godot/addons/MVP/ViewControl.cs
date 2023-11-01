using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

[Tool]
public partial class ViewControl : Control
{
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            var presentMgr = GetNodeOrNull<PresentManager>("PresentManager");
            if(presentMgr == null)
            {
                var scene = GD.Load<PackedScene>("res://addons/MVP/present_manager.tscn");
                presentMgr = scene.Instantiate<PresentManager>();
                presentMgr.Name = "PresentManager";

                AddChild(presentMgr);
                MoveChild(presentMgr, 0);
            }

            presentMgr.SetView(this);
        }

        base._Ready();
    }
}