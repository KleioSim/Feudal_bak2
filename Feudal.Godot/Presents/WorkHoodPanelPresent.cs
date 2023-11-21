using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class WorkHoodPanelPresent : Present<WorkHoodPanelView, ISession>
{
    protected override void InitialConnects()
    {
        view.SelectLaborButton.Pressed += () =>
        {

            var workHood = model.WorkHoods.SingleOrDefault(x => x.Id == view.Id);

            switch (workHood)
            {
                case IDiscoverWorkHood discoverWorkHood:
                    {
                        SendUICommand(new DiscoverTerrainCommand()
                        {
                            Position = discoverWorkHood.Position
                        });
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        };
    }

    protected override void Refresh()
    {
        var workHood = model.WorkHoods.SingleOrDefault(x => x.Id == view.Id);

        view.CurrentLabor.SetHidden(workHood.Task == null);
        view.SelectLabor.SetHidden(workHood.Task != null);

        switch (workHood)
        {
            case IDiscoverWorkHood discoverWorkHood:
                {
                    view.DiscoverProgress.SetHidden(false);
                    view.Product.SetHidden(true);

                    view.DiscoverProgress.Value = discoverWorkHood.DiscoverdPercent;
                    view.DiscoverLabel.Text = discoverWorkHood.Task == null ? "未探索" : $"探索中{discoverWorkHood.DiscoverdPercent}";
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }
}