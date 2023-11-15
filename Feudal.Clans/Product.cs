using Feudal.Interfaces;

namespace Feudal.Clans;

class Product : IProduct
{
    public static Dictionary<ProductType, Func<IClan, IEnumerable<(string desc, float value)>>> IncomeDict = new Dictionary<ProductType, Func<IClan, IEnumerable<(string desc, float value)>>>();
    public static Dictionary<ProductType, Func<IClan, IEnumerable<(string desc, float value)>>> OutputDict = new Dictionary<ProductType, Func<IClan, IEnumerable<(string desc, float value)>>>()
    {
        {
            ProductType.Food,
            (clan) =>
            {
                return new (string desc, float value)[] { ("Conusme", clan.PopCount/100f) };
            }
        }
    };

    public ProductType Type { get; }

    public float Current { get; set; }

    public float Income => IncomeDetails.Sum(x => x.value);

    public float Output => OutputDetails.Sum(x => x.value);

    public float Surplus => Income - Output;

    public IEnumerable<(string desc, float value)> IncomeDetails
    {
        get
        {
            if (!IncomeDict.TryGetValue(Type, out var func))
            {
                return Enumerable.Empty<(string desc, float value)>();
            }

            return func(owner);
        }
    }

    public IEnumerable<(string desc, float value)> OutputDetails
    {
        get
        {
            if (!OutputDict.TryGetValue(Type, out var func))
            {
                return Enumerable.Empty<(string desc, float value)>();
            }

            return func(owner);
        }
    }

    private IClan owner;

    public Product(IClan owner, ProductType type)
    {
        this.owner = owner;

        this.Type = type;
    }

    public void Update()
    {
        Current += Surplus;
    }
}