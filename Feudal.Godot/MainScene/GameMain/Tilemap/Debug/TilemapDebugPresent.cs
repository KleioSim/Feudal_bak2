using Feudal.Godot.Presents;
using Feudal.Interfaces;
using Feudal.Interfaces.UICommands;
using Feudal.Messages;
using Feudal.TerrainBuilders;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TilemapDebugPresent : Present<TilemapDebugView, ISession>
{
    private Vector2I curr;

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new List<ITerrain>()
    };

    private List<ITerrain> Terrains => model.Terrains as List<ITerrain>;
    private TerrainType MapType => Enum.Parse<TerrainType>(view.SelectedTerrainType);

    private (int x, int y) currPos;
    private TerrainBuilder terrainBuilder;

    protected override void InitialConnects()
    {
        view.Timer.Timeout += () =>
        {
            if (Math.Abs(currPos.y) >= view.MapSize || Math.Abs(currPos.x) >= view.MapSize)
            {
                view.Timer.Stop();
                return;
            }

            var terrainType = terrainBuilder.Build(currPos);

            Terrains.Add(new TerrainMock() { Position = currPos, TerrainType = terrainType });

            currPos = GetNextPosition(currPos);

            SendUICommand(new MESSAGE_MockUpdate());
        };

        view.Generate.Pressed += () =>
        {
            Terrains.Clear();

            terrainBuilder = new TerrainBuilder(MapType);

            currPos = (0, 0);

            view.Timer.Start();
        };

        view.TilemapView.ClickTile += (Vector2I index) =>
        {
            var pos = (index.X, index.Y);
            if (Terrains.Any(x => x.Position == pos))
            {
                return;
            }

            var terrainType = terrainBuilder.Build(currPos);

            Terrains.Add(new TerrainMock() { Position = pos, TerrainType = terrainType });

            SendUICommand(new MESSAGE_MockUpdate());
        };
    }

    private (int x, int y) GetNextPosition((int x, int y) Position)
    {
        if (Position.x == 0 && Position.y == 0)
        {
            return (0, 1);
        }

        if (Position.x == 1 && Position.y > 0)
        {
            return (0, Position.y + 1);
        }

        if (Math.Abs(Position.x) < Math.Abs(Position.y))
        {
            if (Position.y > 0)
            {
                return (Position.x - 1, Position.y);
            }
            else
            {
                return (Position.x + 1, Position.y);
            }
        }

        if (Math.Abs(Position.y) < Math.Abs(Position.x))
        {
            if (Position.x > 0)
            {
                return (Position.x, Position.y + 1);
            }
            else
            {
                return (Position.x, Position.y - 1);
            }
        }

        if (Position.x < 0 && Position.y > 0)
        {
            return (Position.x, Position.y - 1);
        }
        if (Position.x < 0 && Position.y < 0)
        {
            return (Position.x + 1, Position.y);
        }
        if (Position.x > 0 && Position.y < 0)
        {
            return (Position.x, Position.y + 1);
        }
        if (Position.x > 0 && Position.y > 0)
        {
            return (Position.x - 1, Position.y);
        }

        throw new Exception();
    }

    protected override void Refresh()
    {

    }
}