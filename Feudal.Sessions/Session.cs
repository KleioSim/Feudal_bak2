using Feudal.Clans;
using Feudal.Interfaces;
using Feudal.MessageBuses;
using Feudal.MessageBuses.Interfaces;
using Feudal.Terrains;
using Feudal.WorkHoods;
using Tasks;

namespace Feudal.Sessions;

internal class Session : ISession
{
    public IDate Date { get; }

    public IClan PlayerClan => Clans.First();

    public IEnumerable<ITask> Tasks => taskManager;

    public IEnumerable<IClan> Clans => clanManager;
    public IEnumerable<ITerrain> Terrains => terrainManager;
    public IEnumerable<IWorkHood> WorkHoods => workHoodManager;

    private IMessageBus messageBus;

    private ClanManager clanManager;
    private TerrainManager terrainManager;
    private WorkHoodManager workHoodManager;
    private TaskManager taskManager;

    public void ProcessUICommand(IMessage message)
    {
        messageBus.PostMessage(message);
    }

    public Session()
    {
        messageBus = new MessageBus();

        Date = new Date(messageBus);
        clanManager = new ClanManager(messageBus);
        terrainManager = new TerrainManager(messageBus);
        workHoodManager = new WorkHoodManager(messageBus);
        taskManager = new TaskManager(messageBus);

        terrainManager.GenerateMap();
    }
}
