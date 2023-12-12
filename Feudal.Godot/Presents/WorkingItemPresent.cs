using Feudal.Interfaces;
using System;

namespace Feudal.Godot.Presents;

partial class WorkingItemPresent : Present<WorkingItemView, ISession>
{

    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        switch (view.Id)
        {
            case IProductWorking productWorking:
                UpdateProductWorkItem(view, productWorking);
                break;
            case IProgressWorking progressWorking:
                UpdateProgressWorkItem(view, progressWorking);
                break;
            default:
                throw new Exception();
        }
    }

    private void UpdateProgressWorkItem(WorkingItemView workingItem, IProgressWorking working)
    {
        workingItem.Button.Text = working.Name;

        workingItem.ProgressPanel.SetHidden(false);
        workingItem.ProductPanel.SetHidden(true);

        workingItem.ProgressBar.Value = working.Percent;
    }

    private void UpdateProductWorkItem(WorkingItemView workingItem, IProductWorking working)
    {
        workingItem.Button.Text = working.Name;

        workingItem.ProgressPanel.SetHidden(true);
        workingItem.ProductPanel.SetHidden(false);

        workingItem.ProductType.Text = working.ProductType.ToString();
        workingItem.ProductCount.Text = working.ProductCount.ToString();
    }
}