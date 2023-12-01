using Feudal.Interfaces;

namespace Feudal.WorkHoods;

class DiscoverWorkHood : IDiscoverWorkHood
{
    public static Action<DiscoverWorkHood> TerrainDiscovered;

    public static int Count = 0;

    private int discoverdPercent;
    public int DiscoverdPercent
    {
        get
        {
            return discoverdPercent;
        }
        set
        {
            discoverdPercent = value;
            if (discoverdPercent >= 100)
            {
                TerrainDiscovered.Invoke(this);
            }
        }
    }

    public string Id { get; } = $"WorkHood_{Count++}";

    public (int x, int y) Position { get; init; }
}