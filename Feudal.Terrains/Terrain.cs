using Feudal.Interfaces;

namespace Feudal.Terrains;

class Terrain : ITerrain
{
    public (int x, int y) Position { get; init; }

    public TerrainType TerrainType { get; set; }

    public bool IsDiscoverd { get; set; }

    public string WorkHoodId { get; private set; }
}

class DiscoverWorkHood : IDiscoverWorkHood
{
    public int DiscoverdPercent { get; set; }

    public ITask Task { get; set; }

    public string Id { get; }

    public (int x, int y) Position => throw new NotImplementedException();
}