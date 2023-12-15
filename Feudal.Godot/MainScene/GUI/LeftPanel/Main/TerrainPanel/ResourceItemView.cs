using Godot;

public partial class ResourceItemView : ItemView
{
    public override object Id { get; set; }

    public Label Label => GetNode<Label>("Item/Label");
}