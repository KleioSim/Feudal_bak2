using Feudal.Interfaces;

namespace Feudal.WorkHoods;

class DiscoverWorkHood : WorkHood, IDiscoverWorkHood
{
    public static Action<DiscoverWorkHood> TerrainDiscovered;

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

    public (int x, int y) Position { get; init; }
}

class EstateWorkHood : WorkHood, ITerrainWorkHood
{
    public (int x, int y) Position { get; init; }
}

abstract class WorkHood : IWorkHood
{
    public static int Count = 0;
    public string Id { get; } = $"WorkHood_{Count++}";

    public IWorking CurrentWorking => throw new NotImplementedException();

    public IEnumerable<IWorking> OptionWorkings => throw new NotImplementedException();
}