using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

internal partial class ResourceItemPresent
{
    protected override ISession MockModel
    {
        get
        {
            view.Id = Resource.FatSoild;
            return new SessionMock();
        }
    }
}