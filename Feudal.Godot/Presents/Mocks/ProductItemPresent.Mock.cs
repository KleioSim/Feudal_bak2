using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
public partial class ProductItemPresent
{
    private const ProductType defaultId = Feudal.Interfaces.ProductType.Food;

    private string productType = Enum.GetName(defaultId.GetType(), defaultId);

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

    protected override ISession MockModel
    {
        get
        {
            var clan = new ClanMock();

            var product = clan.Products[defaultId] as ProductMock;
            product.Current = 10f;
            product.Surplus = 2.1f;

            view.Id = defaultId;

            var mock = new SessionMock();
            mock.ClanMocks.Add(clan);

            return mock;
        }
    }
}