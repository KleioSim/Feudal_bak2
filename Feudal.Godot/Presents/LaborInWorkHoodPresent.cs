using Feudal.Interfaces;
using Feudal.Messages;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class LaborInWorkHoodPresent : Present<LaborInWorkHoodView, ISession>
{
    protected override void InitialConnects()
    {
        view.AssginLabor += (laborId) =>
        {
            SendUICommand(new MESSAGE_StartTask()
            {
                WorkHoodId = view.WorkHoodId,
                LaborId = laborId
            });
        };

        view.CancelLaborButton.Pressed += () =>
        {
            var task = model.Tasks.SingleOrDefault(x => x.WorkHoodId == view.WorkHoodId);
            if (task != null)
            {
                SendUICommand(new MESSAGE_CancelTask()
                {
                    Id = task.Id
                });
            }
        };
    }

    protected override void Refresh()
    {
        var task = model.Tasks.SingleOrDefault(x => x.WorkHoodId == view.WorkHoodId);
        if (task == null)
        {
            view.SelectLabor.SetHidden(false);
            view.CurrentLabor.SetHidden(true);
            return;
        }

        view.SelectLabor.SetHidden(true);
        view.CurrentLabor.SetHidden(false);

        var clan = model.Clans.SingleOrDefault(x => x.Id == task.ClanId);
        if (clan == null)
        {
            throw new Exception();
        }

        view.ClanName.Text = clan.Name;
    }
}