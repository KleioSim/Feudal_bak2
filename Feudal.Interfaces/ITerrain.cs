namespace Feudal.Interfaces;

public interface ITerrain
{
    (int x, int y) Position { get; }

    TerrainType TerrainType { get; }

    bool IsDiscoverd { get; }

    string WorkHoodId { get; }
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
    string Id { get; }
    ITask Task { get; }
}

public interface IDiscoverWorkHood : IWorkHood
{
    (int x, int y) Position { get; }
    int DiscoverdPercent { get; }
}