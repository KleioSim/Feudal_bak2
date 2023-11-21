using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Feudal.Godot.Presents;

public partial class WorkHoodPanelPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        WorkHoods = new List<IWorkHood>
        {
            new DiscoverWorkHood_Mock()
            {
                Id = WorkHoodPanelView.DefaultId
            }
        }
    };

    public bool IsHaveTask
    {
        get
        {
            return model.WorkHoods.First().Task != null;
        }
        set
        {
            var workHood = model.WorkHoods.First() as WorkHood_Mock;
            if (value && workHood.Task == null)
            {
                workHood.Task = new TaskMock();
            }
            if (!value && workHood.Task != null)
            {
                workHood.Task = null;
            }

            isDirty = true;
        }
    }

    public int DiscoveredPercent
    {
        get
        {
            var workHood = model.WorkHoods.First() as DiscoverWorkHood_Mock;
            return workHood.DiscoverdPercent;
        }
        set
        {
            var workHood = model.WorkHoods.First() as DiscoverWorkHood_Mock;
            workHood.DiscoverdPercent = value;

            isDirty = true;
        }
    }

    private string workHoodType = nameof(DiscoverWorkHood_Mock);
    public string WorkHoodType
    {
        get => workHoodType;
        set
        {
            if (workHoodType == value)
            {
                return;
            }

            workHoodType = value;

            var workHoods = model.WorkHoods as List<IWorkHood>;

            if (workHoodType == "NULL")
            {
                workHoods.Clear();
                view.SetWorkHoodId(null);
            }
            else
            {
                var workHood = Activator.CreateInstance(WorkHoodTypeDict[workHoodType]) as WorkHood_Mock;
                workHood.Id = WorkHoodPanelView.DefaultId;

                workHoods.Add(workHood);
                view.SetWorkHoodId(workHood.Id);
            }

            isDirty = true;
        }
    }


    private System.Collections.Generic.Dictionary<string, Type> WorkHoodTypeDict { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.BaseType == typeof(WorkHood_Mock))
        .ToDictionary(x => x.Name, x => x);

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>
        {
            new Dictionary()
            {
                { "name", nameof(WorkHoodType) },
                { "type", (int)Variant.Type.String },
                { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
                { "hint", (int)PropertyHint.Enum },
                { "hint_string", string.Join(",", WorkHoodTypeDict.Keys.Append("NULL")) }
            }
        };

        if (WorkHoodType != "NULL")
        {
            properties.Add(new Dictionary()
            {
                { "name", nameof(IsHaveTask) },
                { "type", (int)Variant.Type.Bool },
                { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
                { "hint", (int)PropertyHint.None },
            });
        }

        if (model.WorkHoods.Any() && model.WorkHoods.First() is DiscoverWorkHood_Mock discoverWorkHood)
        {
            properties.Add(new Dictionary()
            {
                { "name", nameof(DiscoveredPercent) },
                { "type", (int)Variant.Type.Int },
                { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
                { "hint", (int)PropertyHint.None },
            });
        }

        return properties;
    }
}