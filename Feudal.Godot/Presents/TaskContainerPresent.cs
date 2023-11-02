using Feudal.Interfaces;
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

    protected override void Process()
    {
        GD.Print($"{Engine.GetFramesDrawn()} TaskContainerPresent");

        var taskViewDict = view.GetCurrentItems().ToDictionary(x => x.taskId, x => x);
        var taskObjDict = model.Tasks.ToDictionary(x => x.Id, x => x);

        var needRemoves = new Queue<string>(taskViewDict.Keys.Except(taskObjDict.Keys));
        var needAdds = new Queue<string>(taskObjDict.Keys.Except(taskViewDict.Keys));

        while (needAdds.TryDequeue(out string key))
        {
            if (needRemoves.TryDequeue(out string replaceKey))
            {
                var newTaskView = taskViewDict[replaceKey];
                newTaskView.taskId = key;

                newTaskView.SetHidden(false);
            }
            else
            {
                view.GenerateItem(key);
            }
        }

        while (needRemoves.TryDequeue(out string key))
        {
            view.RemoveItem(taskViewDict[key]);
        }
    }
}