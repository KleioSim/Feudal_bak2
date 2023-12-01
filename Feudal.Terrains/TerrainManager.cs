using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using Feudal.TerrainBuilders;
using System.Collections;
using System.Collections.Generic;

namespace Feudal.Terrains;

internal class TerrainManager : IEnumerable<ITerrain>
{
    private List<Terrain> list = new List<Terrain>();
    private Dictionary<(int x, int y), TerrainType> dict = null;

    public IEnumerator<ITerrain> GetEnumerator()
    {
        return ((IEnumerable<Terrain>)list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }

    internal void GenerateMap()
    {
        list.Clear();

        list.AddRange(dict.Select(p => new Terrain() { Position = p.Key, TerrainType = p.Value }));

        foreach (var terrain in list)
        {
            if (Math.Abs(terrain.Position.x) <= 1 && Math.Abs(terrain.Position.y) <= 1)
            {
                terrain.IsDiscoverd = true;
                continue;
            }

            terrain.IsDiscoverd = false;
            var post = messageBus.PostMessage(new MESSAGE_AddDiscoverWorkHood()
            {
                Position = terrain.Position
            });
        }
    }

    private IMessageBus messageBus;

    public TerrainManager(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
        messageBus.Register(this);
    }

    [MessageProcess]
    void OnMESSAGE_TerrainDiscovered(MESSAGE_TerrainDiscoverd message)
    {
        var terrain = list.SingleOrDefault(x => x.Position == message.Position);
        terrain.IsDiscoverd = true;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                var position = (i + terrain.Position.x, j + terrain.Position.y);
                if (list.Any(x => x.Position == position))
                {
                    continue;
                }

                TerrainBuilder.Build(ref dict, TerrainType.Hill, position);

                var newTerrain = new Terrain() { Position = position, TerrainType = dict[position] };
                newTerrain.IsDiscoverd = false;
                list.Add(newTerrain);

                var post = messageBus.PostMessage(new MESSAGE_AddDiscoverWorkHood()
                {
                    Position = newTerrain.Position
                });
            }
        }
    }
}
