using Feudal.Interfaces;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

public partial class ResourceArrayPresent
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Terrains = new List<TerrainMock>()
        {
            new TerrainMock()
            {
                Position = (0, 0),
                TerrainType = Interfaces.TerrainType.Plain,
                Resources = new []
                {
                    Feudal.Interfaces.Resource.FatSoild,
                    Feudal.Interfaces.Resource.CopperLode
                },
            }
        },
    };
}