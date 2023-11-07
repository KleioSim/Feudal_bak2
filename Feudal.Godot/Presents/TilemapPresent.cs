using Feudal.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feudal.Godot.Presents;

internal partial class TilemapPresent : Present<TilemapView, ISession>
{
    protected override ISession MockModel { get; } = new SessionMock();

    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {

    }
}
