using Godot;
using System;

public partial class ProductContainerView : ItemContainer<ProductItemView>
{
    public override InstancePlaceholder ItemPlaceHolder => GetNode<InstancePlaceholder>("Item");
}