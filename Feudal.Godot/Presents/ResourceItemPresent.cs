using Feudal.Interfaces;

namespace Feudal.Godot.Presents;

internal partial class ResourceItemPresent : Present<ResourceItemView, ISession>
{
    protected override ISession MockModel { get; }

    protected override void InitialConnects()
    {

    }

    protected override void Refresh()
    {
        view.Label.Text = view.Id.ToString();
    }
}