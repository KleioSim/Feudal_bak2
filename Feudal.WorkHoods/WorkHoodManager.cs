﻿using Feudal.Interfaces;
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
        };

        this.messageBus = messageBus;
        messageBus.Register(this);
    }

    [MessageProcess]
    string OnMESSAGE_AddDiscoverWorkHood(MESSAGE_AddDiscoverWorkHood msg)
    {
        var workHood = new DiscoverWorkHood()
        {
            Position = msg.Position
        };

        list.Add(workHood);
        return workHood.Id;
    }

    [MessageProcess]
    void OnMESSAGE_RemoveWorkHood(MESSAGE_RemoveWorkHood message)
    {
        list.RemoveAll(x => x.Id == message.Id);
    }

    [MessageProcess]
    IWorkHood OnMESSAGE_FindWorkHood(MESSAGE_FindWorkHood message)
    {
        return list.SingleOrDefault(x => x.Id == message.Id);
    }
}
