using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
public partial class ProductItemPresent
{
    private string productType = nameof(Feudal.Interfaces.ProductType.Food);

    public string ProductType
    {
        get => productType;
        set
        {
            if (productType == value)
            {
                return;
            }

            productType = value;

            view.Id = Enum.Parse<ProductType>(productType);
            isDirty = true;
        }
    }

    [Export]
    public float Current
    {
        get
        {
            return Product.Current;
        }
        set
        {
            Product.Current = value;
            isDirty = true;
        }
    }

    [Export]
    public float Surplus
    {
        get
        {
            return Product.Surplus;
        }
        set
        {
            Product.Surplus = value;
            isDirty = true;
        }
    }

    private ProductMock Product => model.PlayerClan.Products[Enum.Parse<ProductType>(productType)] as ProductMock;

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        properties.Add(new Dictionary()
        {
            { "name", nameof(ProductType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", Enum.GetNames<ProductType>()) }
        });

        return properties;
    }

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new IClan[]
        {
            new ClanMock()
            {
                Products = new System.Collections.Generic.Dictionary<ProductType, IProduct>()
                {
                    {
                        Feudal.Interfaces.ProductType.Food,
                        new ProductMock()
                        {
                            Type = Feudal.Interfaces.ProductType.Food,
                            Current = 10f,
                            Surplus = 2.1f
                        }
                    }
                }
            }
        }
    };
}