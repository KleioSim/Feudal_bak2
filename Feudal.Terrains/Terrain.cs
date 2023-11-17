using Feudal.Interfaces;

namespace Feudal.Terrains;

class Terrain : ITerrain
{
    public (int x, int y) Position { get; init; }

    public TerrainType TerrainType { get; set; }

    public bool IsDiscoverd { get; set; }

    private IWorkHood workHood;
    public IWorkHood WorkHood
    {
        get
        {
            if (!IsDiscoverd && !(workHood is DiscoverWorkHood))
            {
                workHood = new DiscoverWorkHood();
            }

            return workHood;
        }
    }
}

class DiscoverWorkHood : IDiscoverWorkHood
{
    public int DiscoverdPercent { get; set; }

    public ITask Task { get; set; }
}