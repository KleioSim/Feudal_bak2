using Feudal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

internal partial class MainScenePresent : Present<MainSceneView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock()
    {
        Tasks = new List<TaskMock>()
        {
            new TaskMock(),
            new TaskMock()
        },

        Clans = new List<IClan>()
        {
            new ClanMock(){ Name = "PlayerClan_Mock", PopCount = 1000  },
            new ClanMock(),
            new ClanMock(),
        },

        Terrains = new List<TerrainMock>()
        {
            new TerrainMock(){ Position = (0,0), TerrainType = TerrainType.Plain }
        },

        Date = new DateMock()
        {
            Year = 1,
            Month = 1,
            Day = 1
        }
    };
}
