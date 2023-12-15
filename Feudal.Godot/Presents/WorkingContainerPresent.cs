using Feudal.Interfaces;
using Feudal.Messages;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

internal partial class WorkingContainerPresent : Present<WorkingContainerView, ISession>
{
    protected override void InitialConnects()
    {
        view.CurrentWorking.Button.Pressed += () =>
        {
            view.OptionWorkingsPanel.SetHidden(view.OptionWorkingsPanel.Visible);

            if (!view.OptionWorkingsPanel.Visible)
            {
                return;
            }

            var workHood = model.WorkHoods.Single(x => x.Id == view.WorkHoodId);

            var newItems = view.OptionWorkings.Refresh(workHood.OptionWorkings.OfType<object>().ToHashSet());
            foreach (var item in newItems)
            {
                item.Button.Pressed += () =>
                {
                    SendUICommand(new MESSAGE_ChangeWorking() { WorkHoodId = view.WorkHoodId, Working = item.Id });
                };
            }
        };
    }

    protected override void Refresh()
    {
        var workHood = model.WorkHoods.Single(x => x.Id == view.WorkHoodId);
        view.CurrentWorking.Id = workHood.CurrentWorking;

        UpdateWorkItem(view.CurrentWorking);
    }

    private void UpdateWorkItem(WorkingItemView workingItem)
    {
        var workingObj = workingItem.Id as IWorking;
        switch (workingItem.Id)
        {
            case IProductWorking productWorking:
                UpdateProductWorkItem(workingItem, productWorking);
                break;
            case IProgressWorking progressWorking:
                UpdateProgressWorkItem(workingItem, progressWorking);
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
