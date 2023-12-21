using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Feudal.MessageBuses.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class GUIPresent : Present<GUIView, ISession>
{
    [Export]
    public string PlayerClanName
    {
        get => MockModel.PlayerClan.Name;
        set
        {
            ((ClanMock)MockModel.PlayerClan).Name = value;

            isDirty = true;
        }
    }

    [Export]
    public int PlayerClanPopCount
    {
        get => MockModel.PlayerClan.PopCount;
        set
        {
            ((ClanMock)MockModel.PlayerClan).PopCount = value;

            isDirty = true;
        }
    }

    protected override ISession MockModel
    {
        get
        {
            var terrain = new TerrainMock()
            {
                Position = (0, 0),
                TerrainType = Interfaces.TerrainType.Plain,
                Resources = new[] { Interfaces.Resource.FatSoild },
                IsDiscoverd = false,
            };

            var workHood = new TerrainWorkHoodMock() { Position = terrain.Position };
            workHood.OptionWorkingMocks.AddRange(new IWorking[]
            {
                new ProgressWorking_Mock(){ Percent = 20 },
                new ProductWorking_Mock() { ProductType = ProductType.Food, ProductCount = 1.0 },
            });
            workHood.CurrentWorking = workHood.OptionWorkingMocks.First();

            var clans = new[]
            {
                new ClanMock(){ PopCount = 1000 },
                new ClanMock(),
                new ClanMock(),
            };

            var task = new TaskMock()
            {
                WorkHoodId = workHood.Id,
                ClanId = clans[0].Id
            };

            var mock = new SessionMock();

            mock.TerrainMocks.Add(terrain);
            mock.WorkHoodMocks.Add(workHood);
            mock.ClanMocks.AddRange(clans);

            mock.TaskMocks.Add(task);

            return mock;
        }
    }

    //{ get; } = new SessionMock()
    //{
    //    //Tasks = new List<TaskMock>()
    //    //{
    //    //    new TaskMock(),
    //    //    new TaskMock()
    //    //},

    //    //Clans = new List<IClan>()
    //    //{
    //    //    new ClanMock(){ Name = "PlayerClan_Mock", PopCount = 1000  },
    //    //    new ClanMock(),
    //    //    new ClanMock(),
    //    //},

    //    //Terrains = new[]
    //    //{
    //    //    new TerrainMock()
    //    //    {
    //    //        Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y),
    //    //        TerrainType = Interfaces.TerrainType.Plain,
    //    //        IsDiscoverd = false,
    //    //    }
    //    //},

    //    Date = new DateMock()
    //    {
    //        Year = 1,
    //        Month = 1,
    //        Day = 1
    //    }
    //};
}

public class SessionMock : ISession
{
    public IClan PlayerClan => Clans.First();
    public IDate Date { get; init; } = new DateMock() { Year = 1, Month = 2, Day = 3 };

    public IEnumerable<IClan> Clans => ClanMocks;
    public IEnumerable<ITask> Tasks => TaskMocks;
    public IEnumerable<ITerrain> Terrains => TerrainMocks;
    public IEnumerable<IWorkHood> WorkHoods => WorkHoodMocks;

    internal List<WorkHood_Mock> WorkHoodMocks { get; } = new List<WorkHood_Mock>();
    internal List<TaskMock> TaskMocks { get; } = new List<TaskMock>();
    internal List<ClanMock> ClanMocks { get; } = new List<ClanMock>();
    internal List<TerrainMock> TerrainMocks { get; } = new List<TerrainMock>();

    public void ProcessUICommand(IMessage command)
    {
        GD.Print($"ProcessUICommand {command.GetType().Name}");
    }

    public SessionMock()
    {

    }
}

public class DateMock : IDate
{
    public int Year { get; init; }

    public int Month { get; init; }

    public int Day { get; init; }
}
