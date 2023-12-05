using Godot;

public partial class ResourceItemView : ItemView
{
    public override object Id { get; set; } = Feudal.Interfaces.Resource.FatSoild;

    public Label Label => GetNode<Label>("Item/Label");
}