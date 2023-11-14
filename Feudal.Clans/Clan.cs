using Feudal.Interfaces;

namespace Feudal.Clans;

internal class Clan : IClan
{
    public static int Count;

    public string Id { get; }

    public string Name { get; set; }

    public int PopCount { get; set; }

    public ILabor Labor => labor;

    private readonly LaborImp labor;

    public Clan()
    {
        Id = $"CLAN_{Count++}";
        Name = Id;

        labor = new LaborImp(this);
    }

    public class LaborImp : ILabor
    {
        private readonly Clan clan;

        public LaborImp(Clan clan)
        {
            this.clan = clan;
        }

        public int TotalCount => clan.PopCount / 100;
    }
}