using Feudal.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
internal partial class TaskContainerPresent : Present.Template<TaskContainerView, ISession>
{
    [Export]
    public int taskCount
    {
        get
        {
            return MockModel.tasks.Count();
        }
        set
        {
            var list = MockModel.tasks as List<TaskMock>;
            if (value > list.Count())
            {
                list.AddRange(Enumerable.Range(0, value - list.Count()).Select(_ => new TaskMock() { Id = Guid.NewGuid().ToString() }));
            }
            while (value < list.Count())
            {
                list.RemoveAt(0);
            }

            isDirty = true;
        }
    }

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
}
