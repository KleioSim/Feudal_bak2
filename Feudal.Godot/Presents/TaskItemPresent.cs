using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

internal partial class TaskItemPresent : Present<TaskItemView, ISession>
{
    private ITask taskObj => model.Tasks.Single(x => x.Id == view.Id);

    protected override void InitialConnects()
    {
        
    }

    protected override void Refresh()
    {
        view.Label.Text = taskObj.Desc;
        view.Progress.Value = taskObj.Percent;
    }
}
