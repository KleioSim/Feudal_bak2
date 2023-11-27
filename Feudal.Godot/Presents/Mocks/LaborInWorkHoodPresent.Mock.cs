using Feudal.Interfaces;
using Feudal.Messages;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class LaborInWorkHoodPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        WorkHoods = new List<IWorkHood>
        {
            new DiscoverWorkHood_Mock()
            {
                Id = DiscoverWorkHoodPanelView.DefaultId
            }
        },

        Clans = new List<IClan>()
        {
            new ClanMock()
            {
                Id = ClanPanelView.DefaultId
            }
        },

        Tasks = new List<ITask>
        {
            new TaskMock()
            {
                WorkHoodId = DiscoverWorkHoodPanelView.DefaultId,
                ClanId = ClanPanelView.DefaultId
            }
        }
    };

    [Export]
    public bool IsHaveTask
    {
        get
        {
            return model.Tasks.Any(x => x.WorkHoodId == view.WorkHoodId);
        }
        set
        {
            var tasks = model.Tasks as List<ITask>;
            if (value)
            {
                if (tasks.Any(x => x.WorkHoodId == view.WorkHoodId))
                {
                    return;
                }

                tasks.Add(new TaskMock()
                {
                    WorkHoodId = view.WorkHoodId,
                    ClanId = ClanPanelView.DefaultId
                });
            }
            else
            {
                tasks.RemoveAll(x => x.WorkHoodId == view.WorkHoodId);
            }

            SendUICommand(new MESSAGE_MockUpdate());
        }
    }
}