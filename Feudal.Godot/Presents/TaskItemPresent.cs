using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

internal partial class TaskItemPresent : Present<TaskItemView, ISession>
{
    private ITask taskObj => model.tasks.Single(x => x.Id == view.taskId);

    protected override void InitialConnects()
    {
        
    }

    protected override void Process()
    {
        GD.Print($"{Engine.GetFramesDrawn()} TaskItemPresent");

        view.Label.Text = taskObj.Desc;
        view.Progress.Value = taskObj.Percent;
    }
}
