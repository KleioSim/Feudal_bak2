namespace Feudal.Interfaces;

public interface ITerrain
{
    (int x, int y) Position { get; }

    TerrainType TerrainType { get; }

    bool IsDiscoverd { get; }
}

public enum TerrainType
{
    Plain,
    Hill,
    Mountion,
    Lake,
    Marsh
}