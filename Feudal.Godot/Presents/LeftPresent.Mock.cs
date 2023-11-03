﻿using Feudal.Interfaces;
using Godot.Collections;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

[Tool]
public partial class LeftPresent : Present<LeftView, ISession>
{
    private string mainPanelType;
    public string MainPanelType
    {
        get => mainPanelType;
        set
        {
            if (mainPanelType == value)
            {
                return;
            }

            mainPanelType = value;

            view.ShowMainPanel(MainPanelView.DerivedTypes.Single(x => x.Name == mainPanelType));
        }
    }

    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        var MainPanelTypes = MainPanelView.DerivedTypes.Select(x => x.Name).ToArray();

        properties.Add(new Dictionary()
        {
            { "name", nameof(MainPanelType) },
            { "type", (int)Variant.Type.String },
            { "usage", (int)PropertyUsageFlags.Default }, // See above assignment.
            { "hint", (int)PropertyHint.Enum },
            { "hint_string", string.Join(",", MainPanelTypes) }
        });

        return properties;
    }

    protected override ISession MockModel => throw new System.NotImplementedException();
}
