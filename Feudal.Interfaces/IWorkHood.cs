namespace Feudal.Interfaces;

public interface IWorkHood
{
    string Id { get; }
}

public interface ITerrainWorkHood : IWorkHood
{
    (int x, int y) Position { get; }
}

public interface IDiscoverWorkHood : ITerrainWorkHood
{
    int DiscoverdPercent { get; set; }
}

public interface IEstateWorkHood : ITerrainWorkHood
{
    int BuildPercent { get; set; }
}
