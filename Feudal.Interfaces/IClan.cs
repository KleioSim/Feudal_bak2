namespace Feudal.Interfaces
{
    public interface IClan
    {
        string Id { get; }

        string Name { get; }

        int PopCount { get; }

        ILabor Labor { get; }
    }

    public interface ILabor
    {
        int TotalCount { get; }
    }
}