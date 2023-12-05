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
    private TerrainBuilder builder = null;

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

        builder = new TerrainBuilder(TerrainType.Hill);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                messageBus.PostMessage(new MESSAGE_AddTerrain()
                {
                    Position = (i, j),
                    IsDiscovered = i <= 1 && j <= 1
                });
            }
        }
    }

    private IMessageBus messageBus;

    public TerrainManager(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
        messageBus.Register(this);
    }

    [MessageProcess]
    void OnMESSAGE_AddTerrain(MESSAGE_AddTerrain message)
    {
        var terrain = builder.Build(message.Position);
        list.Add(new Terrain() { Position = message.Position, TerrainType = terrain, IsDiscoverd = message.IsDiscovered });

        messageBus.PostMessage(new MESSAGE_AddedTerrain()
        {
            Position = message.Position,
            IsDiscoverd = message.IsDiscovered
        });
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

                messageBus.PostMessage(new MESSAGE_AddTerrain()
                {
                    Position = terrain.Position
                });
            }
        }
    }
}
