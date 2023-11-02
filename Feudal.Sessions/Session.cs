using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;

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

    public void ProcessUICommand(UICommand command)
    {

    }
}

class Clan : IClan
{
    public static int Count;

    public string Id { get; }

    public string Name { get; set; }

    public int PopCount { get; set; }

    public Clan()
    {
        Id = $"CLAN_{Count++}";
        Name = Id;
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