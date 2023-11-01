using Feudal.Interfaces;
using Godot;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Godot.Node;

namespace Feudal.Godot.Presents;

[Tool]
internal class TaskContainerPresent : Present<TaskContainerView, ISession>
{
    public override ISession MockModel { get; } = new SessionMock()
    {
        tasks = new List<TaskMock>()
        {
            new TaskMock()
            {
                Id = "TASK1"
            },
            new TaskMock()
            {
                Id = "TASK2"
            },
        }
    };

    protected override void InitialConnects(TaskContainerView view)
    {
        //throw new System.NotImplementedException();
    }

    protected override void Refresh(TaskContainerView view, ISession model)
    {
        var taskViewDict = view.GetCurrentItems().ToDictionary(x => x.taskId, x => x);
        var taskObjDict = model.tasks.ToDictionary(x => x.Id, x => x);

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


public class TaskMock : ITask
{
    public string Id { get; set; }
}