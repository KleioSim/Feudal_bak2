using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using System.Collections;

namespace Feudal.WorkHoods;

public class WorkHoodManager : IEnumerable<IWorkHood>
{
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
        DiscoverWorkHood.TerrainDiscovered = (workHood) =>
        {
            messageBus.PostMessage(new MESSAGE_TerrainDiscoverd()
            {
                Position = workHood.Position
            });

            list.Remove(workHood);

            UpdateTerrainWorkHoodByResource(workHood.Position);
        };

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
        if (message.IsDiscoverd)
        {
            UpdateTerrainWorkHoodByResource(message.Position);
        }
        else
        {
            list.Add(new DiscoverWorkHood() { Position = message.Position });
        }
    }


    private void UpdateTerrainWorkHoodByResource((int x, int y) position)
    {
        var terrain = messageBus.PostMessage(new MESSAGE_FindTerrain() { Position = position }).WaitAck<ITerrain>();
    }
}
