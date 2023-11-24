using Godot;
using System;

public partial class ProductTopInfoView : ViewControl
{
    internal ItemContainer<ProductItemView> Container { get; }

    public ProductTopInfoView()
    {
        Container = new ItemContainer<ProductItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("Containter/Item");
        });
    }
}
