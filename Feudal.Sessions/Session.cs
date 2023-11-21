using Feudal.Clans;
using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Feudal.MessageBuses;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using Feudal.Terrains;
using Feudal.WorkHoods;
using System;
using System.Collections;

namespace Feudal.Sessions;

internal class Session : ISession
{
    public IDate Date { get; }

    public IClan PlayerClan => Clans.First();

    public IEnumerable<ITask> Tasks { get; } = new List<ITask>()
    {
        new Task(),
        new Task()
    };

    public IEnumerable<IClan> Clans => clanManager;
    public IEnumerable<ITerrain> Terrains => terrainManager;
    public IEnumerable<IWorkHood> WorkHoods => workHoodManager;

    private IMessageBus messageBus;

    private ClanManager clanManager;
    private TerrainManager terrainManager;
    private WorkHoodManager workHoodManager;

    public void ProcessUICommand(IMessage message)
    {
        messageBus.PostMessage(message);
    }

    public Session()
    {
        messageBus = new MessageBus();

        Date = new Date(messageBus);
        clanManager = new ClanManager(messageBus);
        terrainManager = new TerrainManager(messageBus);
        workHoodManager = new WorkHoodManager(messageBus);

        terrainManager.GenerateMap();
    }
}

class Task : ITask
{
    public static int Count;

    public string Id { get; set; }

    public string Desc { get; set; }

    public int Percent { get; set; }

    public Task()
    {
        Id = $"TASK_{Count++}";

        Desc = Id;
        Percent = 0;
    }
}
