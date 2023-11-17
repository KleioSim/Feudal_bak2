using Feudal.Interfaces;
using System;
using System.Linq;
using System.Reflection.Emit;

namespace Feudal.Godot.Presents;

internal partial class TerrainPanelPresent : Present<TerrainPanelView, ISession>
{
    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        var terrain = model.Terrains.SingleOrDefault(x => x.Position == (view.TerrainPosition.X, view.TerrainPosition.Y));

        view.Title.Text = terrain.TerrainType.ToString();

        view.BufferContainer.SetHidden(!terrain.IsDiscoverd);

        if (terrain.WorkHood == null)
        {
            view.WorkPanel.SetHidden(true);
            return;
        }

        view.WorkPanel.SetHidden(false);

        view.CurrentLabor.SetHidden(terrain.WorkHood.Task == null);
        view.SelectLabor.SetHidden(terrain.WorkHood.Task != null);

        switch (terrain.WorkHood)
        {
            case IDiscoverWorkHood discoverWorkHood:
                RefreshDiscoverWorkHood(discoverWorkHood);
                break;
            default:
                throw new Exception($"Not support WorkHood type {terrain.WorkHood.GetType().Name}");
        }

        //if (terrain.WorkHood is IDiscoverWorkHood discoverWorkHood)
        //{
        //    view.BufferContainer.SetHidden(true);
        //    view.Product.SetHidden(true);

        //    view.DiscoverProgress.Value = discoverWorkHood.DiscoverdPercent;

        //    view.DiscoverLabel.Text = terrain.WorkHood.Task == null ? "未探索" : $"探索中{discoverWorkHood.DiscoverdPercent}";
        //}
        //else
        //{
        //    view.BufferContainer.SetHidden(false);
        //    view.Product.SetHidden(false);
        //}


    }

    private void RefreshDiscoverWorkHood(IDiscoverWorkHood discoverWorkHood)
    {
        view.Product.SetHidden(true);

        view.DiscoverProgress.Value = discoverWorkHood.DiscoverdPercent;
        view.DiscoverLabel.Text = discoverWorkHood.Task == null ? "未探索" : $"探索中{discoverWorkHood.DiscoverdPercent}";
    }
}