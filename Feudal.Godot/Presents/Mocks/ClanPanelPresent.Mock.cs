using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

public partial class ClanPanelPresent : Present<ClanPanelView, ISession>
{
    protected override ISession MockModel
    {
        get
        {
            var clan = new ClanMock();
            view.ClanId = clan.Id;

            var mock = new SessionMock();
            mock.ClanMocks.Add(clan);

            return mock;
        }
    }
}

public class ClanMock : IClan
{
    public static int Count;

    public string Id { get; set; }

    public string Name { get; set; }

    public int PopCount { get; set; }

    public ILabor Labor => labor;

    public Dictionary<ProductType, IProduct> Products { get; set; } = Enum.GetValues<ProductType>().ToDictionary(e => e, e => new ProductMock() { Type = e } as IProduct);

    private LaborMock labor;

    public ClanMock()
    {
        Id = $"Clan_{Count++}";

        Name = Id;
        PopCount = Count * 1000;
        labor = new LaborMock();
        labor.TotalCount = Count;
    }
}

public class LaborMock : ILabor
{
    public int TotalCount { get; set; }
}

public class ProductMock : IProduct
{
    public ProductType Type { get; set; }

    public float Current { get; set; }

    public float Income { get; set; }

    public float Output { get; set; }

    public float Surplus { get; set; }

    public IEnumerable<(string desc, float value)> IncomeDetails { get; } = new (string, float)[]
    {
        ("Income1", 0.1f),
        ("Income2", 0.2f)
    };

    public IEnumerable<(string desc, float value)> OutputDetails { get; } = new (string, float)[]
    {
        ("Output1", 0.3f),
        ("Output2", 0.4f)
    };
}