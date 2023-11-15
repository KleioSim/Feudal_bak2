using Feudal.Interfaces;
using System;

namespace Feudal.Godot.Presents;

public partial class ProductItemPresent : Present<ProductItemView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var product = model.PlayerClan.Products[(ProductType)view.Id];
        view.Type.Text = product.Type.ToString();
        view.Value.Text = product.Current.ToString();
        view.Surplus.Text = (product.Surplus >= 0 ? "+" : "") + product.Surplus.ToString("0.0");
    }
}

