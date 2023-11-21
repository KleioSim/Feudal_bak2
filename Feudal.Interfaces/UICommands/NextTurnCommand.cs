namespace Feudal.Interfaces.UICommands;

public class UICommand
{

}

public class NextTurnCommand : UICommand
{
}


public class DiscoverTerrainCommand : UICommand
{
    public (int x, int y) Position { get; init; }
}