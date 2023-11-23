using Godot;

public partial class ProductContainer : ItemContainer<ProductItemView>
{
    public override InstancePlaceholder ItemPlaceHolder => GetNode<InstancePlaceholder>("Item");
}