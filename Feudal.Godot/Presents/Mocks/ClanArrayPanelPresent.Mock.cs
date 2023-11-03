using Feudal.Interfaces;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Feudal.Godot.Presents;


[Tool]
internal partial class ClanArrayPanelPresent : Present<ClanArrayPanelView, ISession>
{
    [Export]
    public int TaskCount
    {
        get
        {
            return MockModel.Clans.Count();
        }
        set
        {
            var list = MockModel.Clans as List<ClanMock>;
            if (value > list.Count())
            {
                list.AddRange(Enumerable.Range(0, value - list.Count()).Select(_ => new ClanMock()));
            }
            while (value < list.Count())
            {
                list.RemoveAt(0);
            }

            isDirty = true;
        }
    }

    protected override ISession MockModel { get; } = new SessionMock()
    {
        Clans = new List<ClanMock>()
        {
            new ClanMock(),
            new ClanMock(),
        }
    };
}