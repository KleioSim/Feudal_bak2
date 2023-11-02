using Feudal.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

[Tool]
internal partial class TaskItemPresent : Present<TaskItemView, ISession>
{
    [Export]
    public int Precent
    {
        get
        {
            return taskObj.Percent;
        }
        set
        {
            (taskObj as TaskMock).Percent = value;

            isDirty = true;
        }
    }

    [Export]
    public string Desc
    {
        get
        {
            return (taskObj as TaskMock).Desc;
        }
        set
        {
            (taskObj as TaskMock).Desc = value;

            isDirty = true;
        }
    }


    protected override ISession MockModel { get; } = new SessionMock()
    {
        tasks = new List<TaskMock>()
        {
            new TaskMock(){ Id = "TASK_DEFAULT" }
        }
    };
}