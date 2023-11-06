using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

internal partial class ClanArrayPanelPresent : Present<ClanArrayPanelView, ISession>
{
    protected override void InitialConnects()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Refresh()
    {
        var dict = view.ItemPlaceHolder.GetSignalList();

        GD.Print($"{Engine.GetFramesDrawn()} TaskContainerPresent");

        var taskViewDict = view.GetCurrentItems().ToDictionary(x => x.Id, x => x);
        var taskObjDict = model.Clans.ToDictionary(x => x.Id, x => x);

        var needRemoves = new Queue<string>(taskViewDict.Keys.Except(taskObjDict.Keys));
        var needAdds = new Queue<string>(taskObjDict.Keys.Except(taskViewDict.Keys));

        while (needAdds.TryDequeue(out string key))
        {
            if (needRemoves.TryDequeue(out string replaceKey))
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

        while (needRemoves.TryDequeue(out string key))
        {
            view.RemoveItem(taskViewDict[key]);
        }
    }
}