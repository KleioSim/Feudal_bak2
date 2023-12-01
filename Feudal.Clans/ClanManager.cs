using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using System.Collections;

namespace Feudal.Clans;

internal class ClanManager : IEnumerable<IClan>
{
    private List<Clan> list = new List<Clan>();

    private IMessageBus messageBus;

    public ClanManager(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
        messageBus.Register(this);

        list.Add(new Clan());
        list.Add(new Clan());
    }

    public IEnumerator<IClan> GetEnumerator()
    {
        return ((IEnumerable<Clan>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }

    [MessageProcess]
    void OnMESSAGE_NextTurn(MESSAGE_NextTurn msg)
    {
        foreach (var clan in list)
        {
            clan.PopCount++;
            foreach (Product product in clan.Products.Values)
            {
                product.Update();
            }
        }
    }


    [MessageProcess]
    IClan OnMESSAGE_FindClan(MESSAGE_FindClan message)
    {
        return list.SingleOrDefault(x => x.Id == message.Id);
    }
}
