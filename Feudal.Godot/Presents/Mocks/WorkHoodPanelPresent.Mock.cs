using Feudal.Interfaces;
using Godot;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class WorkHoodPanelPresent
{
    private bool isHaveLabor = true;
    [Export]
    public bool IsHaveLabor
    {
        get
        {
            return isHaveLabor;
        }
        set
        {
            if (isHaveLabor == value)
            {
                return;
            }

            isHaveLabor = value;
            if (isHaveLabor)
            {
                var task = new TaskMock()
                {
                    WorkHoodId = view.Id,
                    ClanId = model.Clans.First().Id
                };

                ((SessionMock)model).TaskMocks.Add(task);
            }
            else
            {
                ((SessionMock)model).TaskMocks.RemoveAll(x => x.WorkHoodId == view.Id);
            }

            isDirty = true;
        }
    }

    protected override ISession MockModel
    {
        get
        {
            var optionWorkings = new IWorking[]
            {
                new ProgressWorking_Mock(){ Percent = 20 },
                new ProductWorking_Mock() { ProductType = ProductType.Food, ProductCount = 1.0 },
                new ProductWorking_Mock() { ProductType = ProductType.Bronze, ProductCount = 1.4 },
            };

            var workHood = new WorkHood_Mock();
            workHood.CurrentWorking = optionWorkings[0];
            workHood.OptionWorkingMocks.AddRange(optionWorkings);

            view.Id = workHood.Id;

            var clan = new ClanMock();
            var task = new TaskMock()
            {
                WorkHoodId = workHood.Id,
                ClanId = clan.Id
            };

            var mock = new SessionMock();

            mock.WorkHoodMocks.Add(workHood);
            mock.ClanMocks.Add(clan);
            mock.TaskMocks.Add(task);

            return mock;
        }
    }
}