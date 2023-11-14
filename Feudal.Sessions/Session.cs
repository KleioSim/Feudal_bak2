using Feudal.Clans;
using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Feudal.MessageBuses;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using System;
using System.Collections;

namespace Feudal.Sessions;

internal class Session : ISession
{
    public IClan PlayerClan => Clans.First();

    public IEnumerable<ITask> Tasks { get; } = new List<ITask>()
    {
        new Task(),
        new Task()
    };
    public IEnumerable<IClan> Clans => clanManager;

    public IEnumerable<ITerrain> Terrains { get; } = new List<ITerrain>()
    {

    };

    public IDate Date { get; }

    private IMessageBus messageBus;

    private ClanManager clanManager;

    public void ProcessUICommand(UICommand command)
    {
        messageBus.PostMessage(new MESSAGE_NextTurn());
    }

    public Session()
    {
        messageBus = new MessageBus();

        Date = new Date(messageBus);
        clanManager = new ClanManager(messageBus);
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
