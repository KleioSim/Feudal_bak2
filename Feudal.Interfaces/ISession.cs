using Feudal.Interfaces.UICommands;

namespace Feudal.Interfaces;

public interface ISession
{
    IClan PlayerClan { get; }

    IEnumerable<IClan> Clans { get; }
    IEnumerable<ITask> Tasks { get; }
    IEnumerable<ITerrain> Terrains { get; }

    void ProcessUICommand(UICommand command);
}
