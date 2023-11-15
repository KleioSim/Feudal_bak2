using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ProductContainerPresent : Present<ProductContainerView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var dict = view.ItemPlaceHolder.GetSignalList();

        var taskViewDict = view.GetCurrentItems().ToDictionary(x => x.Id, x => x);
        var taskObjDict = model.PlayerClan.Products;

        var needRemoves = new Queue<object>(taskViewDict.Keys.Except(taskObjDict.Keys.OfType<object>()));
        var needAdds = new Queue<object>(taskObjDict.Keys.OfType<object>().Except(taskViewDict.Keys));

        while (needAdds.TryDequeue(out object key))
        {
            if (needRemoves.TryDequeue(out object replaceKey))
            {
                var newTaskView = taskViewDict[replaceKey];
                newTaskView.Id = key;

                newTaskView.SetHidden(false);
            }
            else
            {
                view.AddItem(key);
            }
        }

        while (needRemoves.TryDequeue(out object key))
        {
            view.RemoveItem(taskViewDict[key]);
        }
    }
}
