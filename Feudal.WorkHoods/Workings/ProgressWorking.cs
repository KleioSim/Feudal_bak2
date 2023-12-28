using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;

namespace Feudal.WorkHoods.Workings;

class ProgressWorking : Working, IProgressWorking
{
    public IProgressWorkingDef _def => def as IProgressWorkingDef;

    private IWorkHood workHood;

    public ProgressWorking(IWorkHood workHood)
    {
        this.workHood = workHood;
    }

    public override void Do()
    {
        Percent += _def.Cost;

        if (Percent >= 100)
        {
            OnFinished();
        }
    }

    void OnFinished()
    {
        switch (_def.Type)
        {
            case ProgressType.Discover:
                SendMessage(new MESSAGE_TerrainDiscoverd() { Position = ((TerrainWorkHood)workHood).Position });
                break;
            default:
                throw new Exception();
        }
    }
}