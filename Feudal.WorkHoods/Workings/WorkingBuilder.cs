using Feudal.Interfaces;

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
