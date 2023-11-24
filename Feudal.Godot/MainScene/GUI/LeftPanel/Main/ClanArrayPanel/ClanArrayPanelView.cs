using Feudal.Interfaces;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class ClanArrayPanelView : MainPanelView
{
    internal ItemContainer<ClanItemView> Container;

    public ClanArrayPanelView()
    {
        Container = new ItemContainer<ClanItemView>(() =>
        {
            return this.GetNode<InstancePlaceholder>("DataContainer/VBoxContainer/VBoxContainer/DefaultItem");
        });
    }
}

public class ItemContainer<T> where T : ItemView
{
    private Func<InstancePlaceholder> itemPlaceHolder;

    public ItemContainer(Func<InstancePlaceholder> placeholder)
    {
        this.itemPlaceHolder = placeholder;
    }

    public IEnumerable<T> GetCurrentItems()
    {
        return itemPlaceHolder().GetParent().GetChildren().Select(x => x.GetNodeOrNull<T>("."))
            .Where(x => x != null);
    }

    public T AddItem(object taskId)
    {
        var item = itemPlaceHolder().CreateInstance() as T;
        item.Id = taskId;

        item.SetHidden(false);
        return item;
    }

    public void RemoveItem(T item)
    {
        if (!itemPlaceHolder().GetParent().GetChildren().Contains(item))
        {
            throw new Exception("!this.GetChildren().Contains(item)");
        }

        item.SetHidden(true);
    }

    public IEnumerable<T> Refresh(HashSet<object> keys)
    {
        var taskViewDict = GetCurrentItems().ToDictionary(x => x.Id, x => x);

        var needRemoves = new Queue<object>(taskViewDict.Keys.Except(keys));
        var needAdds = new Queue<object>(keys.Except(taskViewDict.Keys));

        var addedItems = new List<T>();

        while (needAdds.TryDequeue(out object key))
        {
            if (needRemoves.TryDequeue(out object replaceKey))
            {
                var Item = taskViewDict[replaceKey];
                Item.Id = key;

                Item.SetHidden(false);
            }
            else
            {
                var newItem = AddItem(key);
                addedItems.Add(newItem);
            }
        }

        while (needRemoves.TryDequeue(out object key))
        {
            var item = taskViewDict[key];
            RemoveItem(item);
        }

        return addedItems;
    }
}