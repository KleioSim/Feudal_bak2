using Feudal.Interfaces;

namespace Feudal.WorkHoods;


abstract class WorkHood : IWorkHood
{
    public static Func<IWorkingDef, IWorkHood, IWorking> BuildWorking;

    public static int Count { get; private set; } = 0;

    public string Id { get; } = $"WorkHood_{Count++}";

    public IWorking CurrentWorking { get; internal set; }

    public IEnumerable<IWorking> OptionWorkings => optionWorkings;

    private List<IWorking> optionWorkings = new List<IWorking>();

    internal void AddOptionWorking(IWorking working)
    {
        optionWorkings.Add(working);
    }

    internal void RemoveOptionWorking(IWorking working)
    {
        optionWorkings.Remove(working);
    }

    internal void VaildWorkings(IEnumerable<IWorkingDef> workingDefs)
    {
        var needRemoveItems = optionWorkings.Where(x => !workingDefs.Contains(x.def)).ToArray();
        var needAddDefs = workingDefs.Where(x => optionWorkings.All(x => x.def != x)).ToArray();

        foreach (var item in needRemoveItems)
        {
            optionWorkings.Remove(item);

            if (CurrentWorking == item)
            {
                CurrentWorking = null;
            }
        }

        foreach (var def in needAddDefs)
        {
            var newItem = BuildWorking(def, this);
            optionWorkings.Add(newItem);

            CurrentWorking ??= newItem;
        }
    }
}

class TerrainWorkHood : WorkHood, ITerrainWorkHood
{
    public (int x, int y) Position { get; init; }
}
