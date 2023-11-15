﻿using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

internal partial class TaskContainerPresent : Present<TaskContainerView, ISession>
{
    protected override void InitialConnects()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Refresh()
    {
        var taskViewDict = view.GetCurrentItems().ToDictionary(x => x.Id, x => x);
        var taskObjDict = model.Tasks.ToDictionary(x => x.Id, x => x);

        var needRemoves = new Queue<object>(taskViewDict.Keys.Except(taskObjDict.Keys));
        var needAdds = new Queue<object>(taskObjDict.Keys.Except(taskViewDict.Keys));

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
