using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.TerrainBuilders;
using System.Collections;

namespace Feudal.Terrains;

internal class TerrainManager : IEnumerable<ITerrain>
{
    private List<Terrain> list;

    public IEnumerator<ITerrain> GetEnumerator()
    {
        return ((IEnumerable<Terrain>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }


    private IMessageBus messageBus;

    public TerrainManager(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
        messageBus.Register(this);

        list = TerrainBuilder.Build(3, TerrainType.Hill).Select(p => new Terrain() { Position = p.Key, TerrainType = p.Value }).ToList();

        foreach (var terrain in list)
        {
            if (Math.Abs(terrain.Position.x) <= 1 && Math.Abs(terrain.Position.y) <= 1)
            {
                terrain.IsDiscoverd = true;
            }
        }
    }
}
