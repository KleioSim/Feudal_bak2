namespace Feudal.Interfaces.UICommands;

public class NextTurnCommand : UICommand
{
}

public class Message
{

}

public class UICommand : Message
{

}

public class DiscoverTerrainCommand : UICommand
{
    public (int x, int y) Position { get; init; }
}