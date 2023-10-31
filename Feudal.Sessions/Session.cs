using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;

namespace Feudal.Sessions;

internal class Session : ISession
{
    public string PlayerClanName { get; set; } = "Test1";
    public int PlayerClanPopCount { get; set; } = 10000;

    public void ProcessUICommand(UICommand command)
    {
        PlayerClanPopCount++;
    }
}