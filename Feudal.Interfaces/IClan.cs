namespace Feudal.Interfaces
{
    public interface IClan
    {
        string Id { get; }

        string Name { get; }

        int PopCount { get; }

        ILabor Labor { get; }

        Dictionary<ProductType, IProduct> Products { get; }
    }

    public interface ILabor
    {
        int TotalCount { get; }
    }

    public interface IProduct
    {
        ProductType Type { get; }

        float Current { get; }

        float Income { get; }
        float Output { get; }
        float Surplus { get; }

        IEnumerable<(string desc, float value)> IncomeDetails { get; }
        IEnumerable<(string desc, float value)> OutputDetails { get; }
    }

    public enum ProductType
    {
        Food,
        Bronze
    }
}