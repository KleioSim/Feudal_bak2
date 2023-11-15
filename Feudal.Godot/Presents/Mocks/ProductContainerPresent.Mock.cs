using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

public partial class ProductContainerPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new IClan[]
        {
            new ClanMock()
            {
                Products = new System.Collections.Generic.Dictionary<ProductType, IProduct>()
                {
                    {
                        ProductType.Food,
                        new ProductMock()
                        {
                            Type = ProductType.Food,
                            Current = 10f,
                            Surplus = 1.1f
                        }
                    },
                    {
                        ProductType.Bronze,
                        new ProductMock()
                        {
                            Type = ProductType.Bronze,
                            Current = 20f,
                            Surplus = -2.235f
                        }
                    }
                }
            }
        }
    };
}