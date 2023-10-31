using Feudal.Interfaces.UICommands;

namespace Feudal.Interfaces;

public interface ISession
{
    string PlayerClanName { get; }
    int PlayerClanPopCount { get; }

    void ProcessUICommand(UICommand command);
}