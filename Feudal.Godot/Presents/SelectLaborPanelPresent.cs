using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

public partial class SelectLaborPanelPresent : Present<SelectLaborPanelView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new List<ClanMock>()
        {
            new ClanMock(){ PopCount = 1000 },
            new ClanMock(){ PopCount = 2000 },
        }
    };

    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var addItems = view.Container.Refresh(model.Clans.Select(x => x.Id as object).ToHashSet());
        foreach (var item in addItems)
        {
            item.Button.Pressed += () =>
            {
                view.EmitSignal(SelectLaborPanelView.SignalName.SelectedLabor, item.Id as string);
            };
        }
    }
}