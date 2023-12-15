using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

public partial class LaborItemPresent
{
    protected override ISession MockModel
    {
        get
        {
            var clan = new ClanMock();
            view.Id = clan.Id;

            var mock = new SessionMock();
            mock.ClanMocks.Add(clan);

            return mock;
        }
    }
}