using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;

namespace Feudal.Sessions;

class Date : IDate
{
    public int Year { get; private set; }

    public int Month { get; private set; }

    public int Day { get; private set; }

    private IMessageBus messageBus;

    public Date(IMessageBus messageBus)
    {
        Year = 1;
        Month = 1;
        Day = 1;

        this.messageBus = messageBus;
        messageBus.Register(this);
    }

    [MessageProcess]
    void OnMESSAGE_NextTurn(MESSAGE_NextTurn msg)
    {
        Day += 10;

        if (Day > 30)
        {
            Month += 1;
            Day = 1;
        }

        if (Month > 12)
        {
            Year += 1;
            Month = 1;
        }
    }
}