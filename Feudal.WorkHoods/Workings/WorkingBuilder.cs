using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;

namespace Feudal.WorkHoods.Workings;

static class WorkingBuilder
{
    internal static IWorking Build(IWorkingDef def, IWorkHood workHood)
    {
        switch (def)
        {
            case IProductWorkingDef:
                return new ProductWorking() { def = def };
            case IProgressWorkingDef:
                return new ProgressWorking(workHood) { def = def };
            default:
                throw new Exception();
        }
    }
}

abstract class Working : IWorking
{
    public static Action<IMessage> SendMessage { get; set; }

    public int Percent { get; set; }

    public string Key => Name;

    public string Name => def.Name;

    public IWorkingDef def { get; init; }

    public abstract void Do();
}