using Feudal.Interfaces;
using Feudal.Messages;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Feudal.Godot.Presents;

partial class WorkingItemPresent
{
    protected override ISession MockModel { get; } = new SessionMock();

    private string workingType;
    public string WorkingType
    {
        get
        {
            return workingType;
        }
        set
        {
            if (workingType == value)
            {
                return;
            }

            workingType = value;
            view.Id = workingDict[workingType];

            SendUICommand(new MESSAGE_MockUpdate());
        }
    }

    private static System.Collections.Generic.Dictionary<string, IWorking> workingDict = new System.Collections.Generic.Dictionary<string, IWorking>()
    {
        {
            nameof(IProductWorking),
            new ProductWorking_Mock()
            {
                ProductType = ProductType.Food,
                ProductCount = 1.0
            }
        },
        {
            nameof(IProgressWorking),
            new ProgressWorking_Mock()
            {
                Percent = 33
            }
        }
    };

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        properties.Add(new Dictionary()
        {
            { "name", nameof(WorkingType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", workingDict.Keys) }
        });

        return properties;
    }

    public override void _Ready()
    {
        WorkingType = workingDict.Keys.First();

        base._Ready();
    }
}