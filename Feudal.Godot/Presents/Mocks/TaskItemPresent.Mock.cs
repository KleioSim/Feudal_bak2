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

    protected override ISession MockModel
    {
        get
        {
            var mock = new SessionMock();

            var task = new TaskMock();
            mock.TaskMocks.Add(task);

            view.Id = task.Id;

            return mock;
        }
    }
}


public class TaskMock : ITask
{
    public static int Count;

    public string Id { get; set; }

    public string Desc { get; set; }

    public int Percent { get; set; }

    public string WorkHoodId { get; set; }

    public string ClanId { get; set; }

    public TaskMock()
    {
        Id = $"TASK{Count++}";

        Desc = Id;
        Percent = 33;
    }

    public void OnNextTurn()
    {
        throw new NotImplementedException();
    }
}