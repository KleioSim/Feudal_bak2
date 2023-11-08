using Feudal.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;

[Tool]
internal partial class TilemapPresent : Present<TilemapView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new List<TerrainMock>() { new TerrainMock() { Position = (0,0), TerrainType = TerrainType.Plain } }
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
                list[i].TerrainType = (TerrainType)(i % Enum.GetValues(typeof(TerrainType)).Length);
            }

            isDirty = true;
        }
    }
}

public class TerrainMock : ITerrain
{
    public (int x, int y) Position { get; set; }

    public TerrainType TerrainType { get; set; }
}