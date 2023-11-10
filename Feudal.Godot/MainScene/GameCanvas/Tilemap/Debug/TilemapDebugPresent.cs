using Feudal.Godot.Presents;
using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class TilemapDebugPresent : Present<TilemapDebugView, ISession>
{
    private Vector2I curr;

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new List<ITerrain>()
    };

    private List<ITerrain> Terrains => model.Terrains as List<ITerrain>;

    private (int x, int y) currPos;
    private TerrainType mapType;

    private Dictionary<(int x, int y), TerrainType> dict = new Dictionary<(int x, int y), TerrainType>();

    protected override void InitialConnects()
    {
        view.Timer.Timeout += () =>
        {
            if (Math.Abs(currPos.y) >= view.MapSize || Math.Abs(currPos.x) >= view.MapSize)
            {
                view.Timer.Stop();
                return;
            }

            TerrainBuilder.Build(ref dict, mapType, currPos);

            Terrains.Add(new TerrainMock() { Position = currPos, TerrainType = dict[currPos] });

            currPos = GetNextPosition(currPos);

            SendUICommand(new UICommand());
        };

        view.Generate.Pressed += () =>
        {
            Terrains.Clear();
            dict.Clear();

            currPos = (0, 0);
            mapType = Enum.Parse<TerrainType>(view.SelectedTerrainType);

            view.Timer.Start();
        };
    }

    private (int x, int y) GetNextPosition((int x, int y) Position)
    {
        if (Position.x == 0 && Position.y == 0)
        {
            return (0, 1);
        }

        if (Position.x == 1 && Position.y > 0)
        {
            return (0, Position.y + 1);
        }

        if (Math.Abs(Position.x) < Math.Abs(Position.y))
        {
            if (Position.y > 0)
            {
                return (Position.x - 1, Position.y);
            }
            else
            {
                return (Position.x + 1, Position.y);
            }
        }

        if (Math.Abs(Position.y) < Math.Abs(Position.x))
        {
            if (Position.x > 0)
            {
                return (Position.x, Position.y + 1);
            }
            else
            {
                return (Position.x, Position.y - 1);
            }
        }

        if (Position.x < 0 && Position.y > 0)
        {
            return (Position.x, Position.y - 1);
        }
        if (Position.x < 0 && Position.y < 0)
        {
            return (Position.x + 1, Position.y);
        }
        if (Position.x > 0 && Position.y < 0)
        {
            return (Position.x, Position.y + 1);
        }
        if (Position.x > 0 && Position.y > 0)
        {
            return (Position.x - 1, Position.y);
        }

        throw new Exception();
    }

    protected override void Refresh()
    {

    }
}

public static class TerrainBuilder
{
    static Dictionary<TerrainType, (float min, float max)> dictTerrainHeight = new Dictionary<TerrainType, (float, float)>()
    {
        {TerrainType.Lake, (-2f, -0.5f) },
        {TerrainType.Marsh, (-0.5f, 0f) },
        {TerrainType.Plain, (0f, 1f) },
        {TerrainType.Hill, (1f, 2f) },
        {TerrainType.Mountion, (2f, 3f)  },
    };

    private static Random random = new Random();

    public static TerrainType ConvertToTerrainType(float value)
    {
        if(value > dictTerrainHeight[TerrainType.Lake].min &&  
            value <= dictTerrainHeight[TerrainType.Lake].max)
        {
            return TerrainType.Lake;
        }
        else if (value > dictTerrainHeight[TerrainType.Marsh].min &&
            value <= dictTerrainHeight[TerrainType.Marsh].max)
        {
            return TerrainType.Marsh;
        }
        else if (value > dictTerrainHeight[TerrainType.Plain].min &&
            value <= dictTerrainHeight[TerrainType.Plain].max)
        {
            return TerrainType.Plain;
        }
        else if (value > dictTerrainHeight[TerrainType.Hill].min &&
            value <= dictTerrainHeight[TerrainType.Hill].max)
        {
            return TerrainType.Hill;
        }
        else if (value > dictTerrainHeight[TerrainType.Mountion].min &&
            value <= dictTerrainHeight[TerrainType.Mountion].max)
        {
            return TerrainType.Mountion;
        }
        else
        {
            throw new Exception($"Not Support Height {value}");
        }
    }

    public static Dictionary<(int x, int y), TerrainType> Build(int mapSize, TerrainType mapType)
    {
        var dict = new Dictionary<(int x, int y), TerrainType>();

        for (int i = (mapSize - 1) * -1; i < mapSize; i++)
        {
            for (int j = (mapSize - 1) * -1; j < mapSize; j++)
            {
                var value = random.Next((int)(dictTerrainHeight.Values.Select(x=>x.min).Min() * 1000), 
                    (int)((dictTerrainHeight.Values.Select(x=>x.max).Max()) * 1000)) / 1000f;

                dict.Add((i,j), ConvertToTerrainType(value));
            }
        }

        return dict;
    }

    public static IEnumerable<(int x, int y)> GetNearby((int x, int y) position)
    {
        return Enumerable.Range(position.x - 1, 3)
            .SelectMany(x => Enumerable.Range(position.y - 1, 3).Select(y => (x, y)))
            .Where(pos => pos != position);
    }

    public static void Build(ref Dictionary<(int x, int y), TerrainType> dict, TerrainType mapType, (int x, int y) position)
    {
        if (position.x == 0 && position.y == 0)
        {
            dict.Add(position, TerrainType.Plain);
            return;
        }
        else if (Math.Abs(position.x) == 1 || Math.Abs(position.x) == 1)
        {
            if(random.Next(1, 101) > 30)
            {
                dict.Add(position, TerrainType.Plain);
                return;
            }
        }
        else
        {
            if (random.Next(1, 101) > 90)
            {
                dict.Add(position, TerrainType.Plain);
                return;
            }
        }

        var randomFactor = random.Next((int)(dictTerrainHeight.Values.Select(x => x.min).Min() * 1000),
            (int)((dictTerrainHeight.Values.Select(x => x.max).Max()) * 1000)) / 1000f;

        var nears = GetNearby(position).ToArray();

        var list = new List<TerrainType>();
        foreach ( var near in nears)
        {
            if(dict.TryGetValue(near, out TerrainType terrainType))
            {
                list.Add(terrainType);
            }
        }

        var nearbyFactor = list.Count == 0? randomFactor : list.Average(x =>
        {
            var hegiht = dictTerrainHeight[x];
            return (hegiht.max + hegiht.min) / 2;
        });

        var mapTypeFactor = (dictTerrainHeight[mapType].max + dictTerrainHeight[mapType].min) / 2;

        var terrain = ConvertToTerrainType(randomFactor * 0.3f + nearbyFactor * 0.3f + mapTypeFactor * 0.4f);


        dict.Add(position, terrain);
    }
}