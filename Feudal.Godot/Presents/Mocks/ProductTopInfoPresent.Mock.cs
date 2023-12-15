using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

public partial class ProductTopInfoPresent
{
    protected override ISession MockModel
    {
        get
        {
            var clan = new ClanMock();

            var mock = new SessionMock();
            mock.ClanMocks.Add(clan);

            return mock;
        }
    }
}