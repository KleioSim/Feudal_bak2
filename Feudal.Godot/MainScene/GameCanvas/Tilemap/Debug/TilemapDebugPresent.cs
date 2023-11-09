using Feudal.Godot.Presents;
using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using System;
using System.Collections.Generic;

public partial class TilemapDebugPresent : Present<TilemapDebugView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new List<ITerrain>()
    };

    protected override void InitialConnects()
    {
        view.Generate.Pressed += () =>
        {
            var list = model.Terrains as  List<ITerrain>;
            list.Clear();

            for (int i=(view.MapSize-1)*-1; i< view.MapSize; i++)
            {
                for (int j = (view.MapSize - 1) * -1; j < view.MapSize; j++)
                {
                    list.Add(new TerrainMock() { Position = (i, j), TerrainType = Enum.Parse<TerrainType>(view.SelectedTerrainType) });
                }
            }

            SendUICommand(new UICommand());
        };
    }

    protected override void Refresh()
    {

    }
}