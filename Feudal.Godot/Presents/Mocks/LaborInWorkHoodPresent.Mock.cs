using Feudal.Interfaces;
using Feudal.Messages;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class LaborInWorkHoodPresent
{
    private ClanMock clan;
    private WorkHood_Mock workHood;


    protected override ISession MockModel
    {
        get
        {
            workHood = new WorkHood_Mock();
            clan = new ClanMock();
            var task = new TaskMock()
            {
                WorkHoodId = workHood.Id,
                ClanId = clan.Id
            };

            view.WorkHoodId = workHood.Id;

            var mock = new SessionMock();
            mock.ClanMocks.Add(clan);
            mock.WorkHoodMocks.Add(workHood);
            mock.TaskMocks.Add(task);
            return mock;
        }
    }


    [Export]
    public bool IsHaveTask
    {
        get
        {
            return model.Tasks.Any(x => x.WorkHoodId == view.WorkHoodId);
        }
        set
        {
            var tasks = model.Tasks as List<TaskMock>;
            if (value)
            {
                if (tasks.Any(x => x.WorkHoodId == view.WorkHoodId))
                {
                    return;
                }

                tasks.Add(new TaskMock()
                {
                    WorkHoodId = view.WorkHoodId,
                    ClanId = clan.Id
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