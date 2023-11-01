using Feudal.Interfaces.UICommands;

namespace Feudal.Interfaces;

public interface ISession
{
    string PlayerClanName { get; }
    int PlayerClanPopCount { get; }

    IEnumerable<ITask> tasks { get; }
    void ProcessUICommand(UICommand command);
}