namespace Feudal.Interfaces
{
    public interface IClan
    {
        string Id { get; }

        public string Name { get; }
        int PopCount { get; }
    }
}