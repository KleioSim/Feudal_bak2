using Feudal.Interfaces.UICommands;
using Feudal.MessageBuses.Interfaces;

namespace Feudal.Interfaces;

public interface ISession
{
    IClan PlayerClan { get; }

    IDate Date { get; }

    IEnumerable<IClan> Clans { get; }
    IEnumerable<ITask> Tasks { get; }
    IEnumerable<ITerrain> Terrains { get; }
    IEnumerable<IWorkHood> WorkHoods { get; }

    void ProcessUICommand(IMessage command);
}


public interface IDate
{
    int Year { get; }
    int Month { get; }
    int Day { get; }
}