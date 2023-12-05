namespace Feudal.Interfaces;

public interface ITerrain
{
    (int x, int y) Position { get; }

    TerrainType TerrainType { get; }

    bool IsDiscoverd { get; }

    IEnumerable<Resource> Resources { get; }
}

public enum Resource
{
    FatSoild,
    CopperLode
}

public enum TerrainType
{
    Plain,
    Hill,
    Mountion,
    Lake,
    Marsh
}