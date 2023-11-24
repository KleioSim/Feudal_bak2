using Feudal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class ProductTopInfoPresent : Present<ProductTopInfoView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        view.Container.Refresh(model.PlayerClan.Products.Keys.OfType<object>().ToHashSet());
    }
}
