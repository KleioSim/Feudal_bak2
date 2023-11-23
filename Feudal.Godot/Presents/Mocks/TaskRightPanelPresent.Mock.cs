using Feudal.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
internal partial class TaskRightPanelPresent : Present<TaskRightPanelView, ISession>
{
    [Export]
    public int taskCount
    {
        get
        {
            return MockModel.Tasks.Count();
        }
        set
        {
            var list = MockModel.Tasks as List<TaskMock>;
            if (value > list.Count())
            {
                list.AddRange(Enumerable.Range(0, value - list.Count()).Select(_ => new TaskMock()));
            }
            while (value < list.Count())
            {
                list.RemoveAt(0);
            }

            isDirty = true;
        }
    }

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Tasks = new List<TaskMock>()
        {
            new TaskMock(),
            new TaskMock(),
        }
    };
}
