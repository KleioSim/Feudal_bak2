using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class DiscoverWorkHoodPanelPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        //WorkHoods = new List<IWorkHood>
        //{
        //    new DiscoverWorkHood_Mock()
        //    {
        //        Id = DiscoverWorkHoodPanelView.DefaultId
        //    }
        //},

        //Clans = new List<IClan>()
        //{
        //    new ClanMock()
        //    {
        //        Id = ClanPanelView.DefaultId
        //    }
        //}
    };

    [Export]
    public int DiscoveredPercent
    {
        get
        {
            var workHood = model.WorkHoods.First() as DiscoverWorkHood_Mock;
            return workHood.DiscoverdPercent;
        }
        set
        {
            var workHood = model.WorkHoods.First() as DiscoverWorkHood_Mock;
            workHood.DiscoverdPercent = value;

            isDirty = true;
        }
    }
}