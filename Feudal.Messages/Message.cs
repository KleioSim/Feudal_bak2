using Feudal.MessageBuses.Interfaces;

namespace Feudal.Messages;

public class MESSAGE_NextTurn : IMessage
{

}

public class MESSAGE_DayInc : IMessage
{
    public readonly int year;
    public readonly int month;
    public readonly int day;

    public MESSAGE_DayInc(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
    }
}

public class MESSAGE_AddDiscoverWorkHood : IMessage
{
    public (int x, int y) Position { get; init; }
}

//public class MESSAGE_StartDiscover : IMessage
//{
//    public string WorkHoodId { get; init; }
//    public string LaborId { get; init; }
//    public (int x, int y) Position { get; init; }
//}

public class MESSAGE_RemoveWorkHood : IMessage
{
    public string Id { get; init; }
}

public class MESSAGE_StartTask : IMessage
{
    public string WorkHoodId { get; init; }
    public string LaborId { get; init; }
}

public class MESSAGE_MockUpdate : IMessage
{

}

public class MESSAGE_CancelTask : IMessage
{
    public string Id { get; init; }
}

public class MESSAGE_FindClan : IMessage
{
    public string Id { get; init; }
}

public class MESSAGE_FindWorkHood : IMessage
{
    public string Id { get; init; }
}

public class MESSAGE_TerrainDiscoverd : IMessage
{
    public (int x, int y) Position { get; init; }
}