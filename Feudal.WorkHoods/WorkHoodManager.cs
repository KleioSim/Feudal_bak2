using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using Feudal.WorkHoods.Workings;
using System.Collections;

namespace Feudal.WorkHoods;

public class WorkHoodManager : IEnumerable<IWorkHood>
{
    public static Func<(int x, int y), ITerrain> GetTerrain;
    public static Func<Resource, IWorkingDef> GetWorkingDef;
    public static Func<TerrainType, IWorkingDef> GetDiscoverWorkingDef;

    private IMessageBus messageBus;

    private List<IWorkHood> list = new List<IWorkHood>();

    public IEnumerator<IWorkHood> GetEnumerator()
    {
        return ((IEnumerable<IWorkHood>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }

    public WorkHoodManager(IMessageBus messageBus)
    {
        WorkHood.BuildWorking = WorkingBuilder.Build;
        Working.SendMessage = (msg) => messageBus.PostMessage(msg);

        this.messageBus = messageBus;
        messageBus.Register(this);
    }

    [MessageProcess]
    IWorkHood OnMESSAGE_FindWorkHood(MESSAGE_FindWorkHood message)
    {
        return list.SingleOrDefault(x => x.Id == message.Id);
    }

    [MessageProcess]
    void OnMESSAGE_AddedTerrain(MESSAGE_AddedTerrain message)
    {
        UpdateTerrainWorkHood(message.Position);
    }

    [MessageProcess]
    void OnMESSAGE_NextTurn(MESSAGE_DateInc message)
    {
        foreach (var workHood in list)
        {
            workHood.CurrentWorking.Do();
        }
    }

    [MessageProcess]
    void OnMESSAGE_TerrainDiscoverd(MESSAGE_TerrainDiscoverd message)
    {
        UpdateTerrainWorkHood(message.Position);
    }

    private void UpdateTerrainWorkHood((int x, int y) position)
    {
        var terrain = GetTerrain(position);

        var workingDefs = terrain.IsDiscoverd ? terrain.Resources.Select(x => GetWorkingDef(x)) : new[] { GetDiscoverWorkingDef(terrain.TerrainType) };
        if (!workingDefs.Any())
        {
            list.RemoveAll(x => (x is TerrainWorkHood) && ((TerrainWorkHood)x).Position == position);
            return;
        }

        var workHood = list.OfType<TerrainWorkHood>().SingleOrDefault(x => x.Position == position);
        if (workHood == null)
        {
            workHood = new TerrainWorkHood() { Position = position };
            list.Add(workHood);
        }

        workHood.VaildWorkings(workingDefs);
    }
}
