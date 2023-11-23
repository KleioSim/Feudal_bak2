using Godot;

public abstract partial class ClanContainer : ItemContainer<ClanItemView>
{
    public override InstancePlaceholder ItemPlaceHolder => GetNode<InstancePlaceholder>("DefaultItem");
}
