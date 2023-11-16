using Feudal.Interfaces;

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
                TerrainType = TerrainType.Plain
            }
        }
    };
}