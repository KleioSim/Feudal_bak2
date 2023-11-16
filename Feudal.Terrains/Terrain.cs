using Feudal.Interfaces;

namespace Feudal.Terrains;

class Terrain : ITerrain
{
    public (int x, int y) Position { get; init; }

    public TerrainType TerrainType { get; set; }

    public bool IsDiscoverd { get; set; }
}