using Godot;

public partial class TaskContainer : ItemContainer<TaskItemView>
{
    public override InstancePlaceholder ItemPlaceHolder => GetNode<InstancePlaceholder>("DefaultItem");
}
