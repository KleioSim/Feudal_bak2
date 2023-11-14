using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Feudal.MessageBuses;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using System;

namespace Feudal.Sessions;

internal class Session : ISession
{
    public IClan PlayerClan => Clans.First();

    public IEnumerable<ITask> Tasks { get; } = new List<ITask>()
    {
        new Task(),
        new Task()
    };
    public IEnumerable<IClan> Clans { get; } = new List<IClan>()
    {
        new Clan(),
        new Clan(),
    };

    public IEnumerable<ITerrain> Terrains { get; } = new List<ITerrain>()
    {

    };

    public IDate Date { get; }

    private IMessageBus messageBus = new MessageBus();

    public void ProcessUICommand(UICommand command)
    {
        messageBus.PostMessage(new MESSAGE_NextTurn());
    }

    public Session()
    {
        Date = new Date(messageBus);
    }
}

class Clan : IClan
{
    public static int Count;

    public string Id { get; }

    public string Name { get; set; }

    public int PopCount { get; set; }

    public ILabor Labor => labor;

    private readonly LaborImp labor;

    public Clan()
    {
        Id = $"CLAN_{Count++}";
        Name = Id;

        labor = new LaborImp(this);
    }

    public class LaborImp : ILabor
    {
        private readonly Clan clan;

        public LaborImp(Clan clan)
        {
            this.clan = clan;
        }

        public int TotalCount => clan.PopCount / 100;
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
