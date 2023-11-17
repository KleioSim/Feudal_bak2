namespace Feudal.Interfaces;

public interface ITerrain
{
    (int x, int y) Position { get; }

    TerrainType TerrainType { get; }

    bool IsDiscoverd { get; }

    IWorkHood WorkHood { get; }
}

public enum TerrainType
{
    Plain,
    Hill,
    Mountion,
    Lake,
    Marsh
}

public interface IWorkHood
{
    ITask Task { get; }
}

public interface IDiscoverWorkHood : IWorkHood
{
    int DiscoverdPercent { get; }
}