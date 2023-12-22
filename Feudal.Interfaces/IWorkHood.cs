namespace Feudal.Interfaces;

public interface IWorkHood
{
    string Id { get; }
    IWorking CurrentWorking { get; }
    IEnumerable<IWorking> OptionWorkings { get; }
}

public interface ITerrainWorkHood : IWorkHood
{
    (int x, int y) Position { get; }
}

public interface IWorking
{
    string Key { get; }
    string Name { get; }
}

public interface IProgressWorking : IWorking
{
    int Percent { get; }
}

public interface IProductWorking : IWorking
{
    ProductType ProductType { get; }
    double ProductCount { get; }
}

public interface IWorkingDef
{

}