using Feudal.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Feudal.Godot.Presents;

internal partial class TerrainPanelPresent : Present<TerrainPanelView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new[]
        {
            new TerrainMock()
            {
                Position = (TerrainPanelView.DefaultPos.X, TerrainPanelView.DefaultPos.Y),
                TerrainType = TerrainType.Plain,
                IsDiscoverd = false,
                WorkHood = new DiscoverWorkHood_Mock()
            }
        }
    };

    [Export]
    public bool IsHaveTask
    {
        get
        {
            return model.Terrains.First().WorkHood != null && model.Terrains.First().WorkHood.Task != null;
        }
        set
        {
            if (model.Terrains.First().WorkHood == null)
            {
                return;
            }

            var workHood = model.Terrains.First().WorkHood as WorkHood_Mock;
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

            var terrain = model.Terrains.First() as TerrainMock;

            if (workHoodType == "NULL")
            {
                terrain.WorkHood = null;
            }
            else
            {
                terrain.WorkHood = Activator.CreateInstance(WorkHoodTypeDict[workHoodType]) as IWorkHood;
            }

            isDirty = true;
        }
    }



    private System.Collections.Generic.Dictionary<string, Type> WorkHoodTypeDict { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.BaseType == typeof(WorkHood_Mock))
        .ToDictionary(x => x.Name, x => x);

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        properties.Add(new Dictionary()
        {
            { "name", nameof(WorkHoodType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", WorkHoodTypeDict.Keys.Append("NULL")) }
        });

        //if (workHoodType != null)
        //{
        //    properties.Add(new Dictionary()
        //    {
        //        { "name", nameof(IsHaveTask) },
        //        { "type", (int)Variant.Type.Bool },
        //        { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
        //        { "hint", (int)PropertyHint.Enum },
        //        { "hint_string", string.Join(",", WorkHoodTypeDict.Keys.Append("NULL")) }
        //    });
        //}

        return properties;
    }
}

public class WorkHood_Mock : IWorkHood
{
    public ITask Task { get; set; }
}

public class DiscoverWorkHood_Mock : WorkHood_Mock, IDiscoverWorkHood
{
    public int DiscoverdPercent { get; set; }
}