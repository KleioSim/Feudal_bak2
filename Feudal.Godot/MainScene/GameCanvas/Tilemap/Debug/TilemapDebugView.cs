using Godot;
using System;

public partial class TilemapDebugView : ViewControl
{
    public string SelectedTerrainType 
    { 
        get
        {
            var checkBox = GetNode<CheckBox>("UICanvasLayer/HBoxContainer/VBoxContainer/Plain/CheckBox");
            return checkBox.ButtonGroup.GetPressedButton().GetParent().Name;
        }
    }

    public int MapSize
    {
        get
        {
            var spinBox = GetNode<SpinBox>("UICanvasLayer/HBoxContainer/VBoxContainer2/SpinBox");
            return (int)spinBox.Value;
        }
    }

    public Button Generate => GetNode<Button>("UICanvasLayer/HBoxContainer/VBoxContainer2/Button");

    public TilemapView TilemapView => GetNode<TilemapView>("Tilemap");
}
