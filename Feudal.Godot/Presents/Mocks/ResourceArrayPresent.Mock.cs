using Feudal.Interfaces;
using System.Collections.Generic;

namespace Feudal.Godot.Presents;

public partial class ResourceArrayPresent
{
    protected override ISession MockModel
    {
        get
        {
            var terrain = new TerrainMock()
            {
                Position = (0, 0),
                TerrainType = Interfaces.TerrainType.Plain,
            };

            terrain.ResourceList.Add(Resource.FatSoild);
            terrain.ResourceList.Add(Resource.CopperLode);

            view.TerrainPos = terrain.Position;

            var mock = new SessionMock();
            mock.TerrainMocks.Add(terrain);

            return mock;
        }
    }
}