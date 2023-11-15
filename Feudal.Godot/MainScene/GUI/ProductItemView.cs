using Godot;

public partial class ProductItemView : ItemView
{
    public override object Id { get; set; } = Feudal.Interfaces.ProductType.Food;

    public Label Type => GetNode<Label>("Item/HBoxContainer/Type");
    public Label Value => GetNode<Label>("Item/HBoxContainer/Value");
    public Label Surplus => GetNode<Label>("Item/HBoxContainer/Surplus");
}
