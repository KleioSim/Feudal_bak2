using Feudal.Interfaces;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class DiscoverWorkHoodPanelPresent : Present<DiscoverWorkHoodPanelView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var workHood = model.WorkHoods.SingleOrDefault(x => x.Id == view.Id) as IDiscoverWorkHood;

        view.DiscoverProgress.Value = workHood.DiscoverdPercent;

        var task = model.Tasks.SingleOrDefault(x => x.WorkHoodId == view.Id);
        view.DiscoverLabel.Text = task == null ? "未探索" : $"探索中 {workHood.DiscoverdPercent}%";
    }
}