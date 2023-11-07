﻿using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
internal partial class TilemapPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new List<TerrainMock>()
    };

    [Export]
    public Vector2[] Positions 
    {
        get
        {
            return model.Terrains.Select(terrain => new Vector2(terrain.Position.x, terrain.Position.y)).ToArray();
        }
        set
        {
            var list = model.Terrains as List<TerrainMock>;

            if(value.Length < list.Count)
            {
                list.RemoveRange(0, list.Count - value.Length);
            }

            for(int i=0; i< value.Length; i++)
            {
                if(list.Count <= i)
                {
                    list.Add(new TerrainMock());
                }

                list[i].Position = ((int)value[i].X, (int)value[i].Y);
            }

            isDirty = true;
        }
    }
}


public class TerrainMock : ITerrain
{
    public (int x, int y) Position { get; set; }
}