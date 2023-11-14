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