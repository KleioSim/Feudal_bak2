using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ClanArrayPanelView : MainPanelView
{
    public ClanContainer clanContainer => GetNode<ClanContainer>("DataContainer/VBoxContainer/VBoxContainer");
}

public abstract partial class ItemContainer<T> : Control
    where T : ItemView
{
    public abstract InstancePlaceholder ItemPlaceHolder { get; }

    public IEnumerable<T> GetCurrentItems()
    {
        return ItemPlaceHolder.GetParent().GetChildren().Select(x => x.GetNodeOrNull<T>("."))
            .Where(x => x != null);
    }

    public T AddItem(object taskId)
    {
        var item = ItemPlaceHolder.CreateInstance() as T;
        item.Id = taskId;

        item.SetHidden(false);
        return item;
    }

    public void RemoveItem(T item)
    {
        if (!ItemPlaceHolder.GetParent().GetChildren().Contains(item))
        {
            throw new Exception("!this.GetChildren().Contains(item)");
        }

        item.SetHidden(true);
    }
}