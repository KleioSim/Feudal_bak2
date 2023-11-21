using Feudal.Interfaces;

namespace Feudal.WorkHoods;

class DiscoverWorkHood : IDiscoverWorkHood
{
    public static int Count = 0;

    public int DiscoverdPercent { get; set; }

    public ITask Task { get; set; }

    public string Id { get; } = $"WorkHood_{Count++}";

    public (int x, int y) Position { get; init; }
}