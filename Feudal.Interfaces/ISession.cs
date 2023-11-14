using Feudal.Interfaces.UICommands;

namespace Feudal.Interfaces;

public interface ISession
{
    IClan PlayerClan { get; }

    IDate Date { get; }

    IEnumerable<IClan> Clans { get; }
    IEnumerable<ITask> Tasks { get; }
    IEnumerable<ITerrain> Terrains { get; }

    void ProcessUICommand(UICommand command);
}


public interface IDate
{
    int Year { get; }
    int Month { get; }
    int Day { get; }
}